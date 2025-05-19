# Visão Geral do Desenvolvedor - 16/05/2025
Este documento fornece uma visão geral abrangente do projeto, incluindo sua estrutura, configuração e instruções de execução.

O documento aborda aspectos técnicos essenciais como:
- Requisitos do ambiente de desenvolvimento
- Opções de execução (Docker e local)
- Configurações de banco de dados
- Apontamentos e explicação técnica de algumas soluções

# Instruções de Setup e Execução do Projeto

## Requisitos
- **.NET 8 SDK** (para rodar localmente)
- **Docker e Docker Compose** (para rodar via containers)
- **Banco de Dados:** PostgreSQL (pode ser via Docker ou local)

---

## Como rodar o projeto com Docker (recomendado)

1. **Clone o repositório:**
   ```sh
   git clone https://github.com/juniorpaiva95/challenge-ambev-backend.git
   cd challenge-ambev-backend/template/backend
   ```
2. **Suba os containers:**
   ```sh
   docker-compose up --build
   ```
   Isso irá:
   - Subir a WebAPI já pronta para uso em http://localhost:8080
   - Subir o banco PostgreSQL, MongoDB e Redis já configurados
   - Executar as migrations automaticamente

3. **Acesse a API:**
   - Swagger: http://localhost:8080/swagger/index.html

---

## Como rodar o projeto localmente (sem Docker)

1. **Configure o banco de dados local:**
   - Instale o PostgreSQL localmente
   - Crie um banco chamado `developer_evaluation` com usuário `developer` e senha `ev@luAt10n`

2. **Configure a string de conexão:**
   - No arquivo `appsettings.Development.json` da WebAPI e da ORM, há duas opções de string de conexão:
     - **Default (Docker):** já aponta para o serviço do Docker Compose
     - **Local:** aponta para `localhost`
   - Para rodar localmente, **copie o conteúdo da string de conexão `LocalDatabase` e cole no lugar da `DefaultConnection`** desse modo não precisará alterar a key nas soluções da WebApi (em `Program.cs`) e na solução ORM (em `DefaultContext.cs`).

   Exemplo:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Host=ambev_developer_evaluation_database;Port=5432;Database=developer_evaluation;Username=developer;Password=ev@luAt10n",
    "LocalDatabase": "Host=localhost;Port=5432;Database=developer_evaluation;Username=developer;Password=ev@luAt10n"
   }
   ```

3. **Rode as migrations:**
    - **<span style="color: red">Não é necessário</span>**, pois ao rodar tanto local quanto no docker as migrations estão automatizadas para executar assim que a aplicação sobe.
   ```sh
   dotnet ef database update --project Ambev.DeveloperEvaluation.ORM/ --startup-project Ambev.DeveloperEvaluation.WebApi/
   ```

4. **Rode a WebAPI:**
   ```sh
   dotnet run --project src/Ambev.DeveloperEvaluation.WebApi
   ```

---

## Observações
- O projeto executa as migrations automaticamente ao iniciar (tanto local quanto no Docker).
- Os dados iniciais (seed) são aplicados automaticamente.

---

## Checklist de Regras de Negócio & Implementação

Abaixo estão as regras de negócio do desafio, com um check (✅) para cada regra implementada, uma explicação e um trecho de código mostrando como ela é aplicada:

### ✅ Compras acima de 4 itens idênticos têm 10% de desconto
### ✅ Compras entre 10 e 20 itens idênticos têm 20% de desconto
### ✅ Compras abaixo de 4 itens não podem ter desconto

**Como está implementado:**
A lógica de desconto é centralizada na classe `DiscountCalculator` em `Domain/Common`:

```csharp
var discount = DiscountCalculator.CalculateDiscount(quantity);
```

Essa classe é utilizada tanto na criação quanto na atualização de vendas, garantindo que o desconto correto seja aplicado de acordo com a quantidade, de forma consistente em toda a aplicação.

---

### ✅ Não é possível vender mais de 20 itens idênticos

**Como está implementado:**
Essa regra é aplicada por validação usando FluentValidation em `CreateSaleItemValidator`:

```csharp
RuleFor(x => x.Quantity)
    .GreaterThan(0)
    .WithMessage("A quantidade deve ser maior que zero.")
    .LessThanOrEqualTo(20)
    .WithMessage("A quantidade máxima por item é 20.");
```

Se uma requisição tentar adicionar mais de 20 unidades do mesmo produto, a API retornará um erro de validação e não processará a venda.

---

## Fluxo de Mapeamento (DTOs e Entidades)

O projeto segue um padrão limpo e desacoplado para o fluxo de dados entre as camadas, utilizando o AutoMapper para conversão entre objetos. O fluxo é o seguinte:

```
Request (WebApi) -> Command (Application) -> Entity (Domain) -> Result (Application) -> Response (WebApi)
```

### Exemplo do fluxo para o caso de uso de Venda:

- **Request:** DTO recebido pela API (ex: `CreateSaleRequest`)
- **Command:** DTO de comando da Application (ex: `CreateSaleCommand`)
- **Entity:** Entidade de domínio (ex: `Sale`)
- **Result:** DTO de resultado da Application (ex: `CreateSaleResult`)
- **Response:** DTO de resposta da API (ex: `CreateSaleResponse`)

### Responsabilidade da camada Application

A camada Application é responsável por:
- Orquestrar o fluxo de dados entre WebApi e Domain
- Validar comandos e regras de negócio
- Mapear comandos para entidades e entidades para resultados
- Coordenar o uso de repositórios e serviços de domínio

### Vantagens desse padrão
- **Clareza:** Cada camada tem seus próprios DTOs, facilitando a manutenção
- **Desacoplamento:** WebApi não depende de Application, que não depende de Domain
- **Facilidade de testes:** Cada camada pode ser testada isoladamente

---------------
### Exceções de Domínio e Tratamento Centralizado

Para garantir um tratamento elegante e padronizado de erros de negócio, foi criada uma exceção base chamada `BusinessDomainException` no domínio. Todas as exceções de regras de negócio (ex: `SaleNotFoundException`, `SaleAlreadyCancelledException`) herdam dessa base.

Essas exceções são capturadas de forma centralizada pelo middleware da WebApi, que retorna uma resposta amigável em JSON e o status HTTP apropriado (por padrão, 409 Conflict). Isso garante que regras de negócio sejam comunicadas de forma clara para o consumidor da API, sem vazar detalhes técnicos.

Exemplo de uso:
- Se uma venda não for encontrada, é lançada `SaleNotFoundException`.
- Se uma venda já estiver cancelada, é lançada `SaleAlreadyCancelledException`.
- Ambas são tratadas pelo middleware e retornam mensagens amigáveis ao cliente.

---------------

## Dados Iniciais (Seeders)

O projeto conta com um esquema de seeders para popular o banco de dados com dados iniciais essenciais para testes e desenvolvimento. Os seeders são implementados como métodos de extensão do ModelBuilder e aplicados no método OnModelCreating do DbContext.

Atualmente, existem seeders para:
- **Produtos:** Popula a tabela de produtos com exemplos de cervejas, refrigerantes e outros itens do portfólio.
- **Usuários:** Popula a tabela de usuários com três perfis genéricos (Administrador, Gerente e Cliente), cada um com e-mail, telefone, perfil e senha já hasheada.

Esses seeders garantem que, ao rodar as migrations, o banco já estará pronto para uso com dados básicos, facilitando o desenvolvimento, testes e validação das regras de negócio.

---------------

## Dificuldade ao Rodar o WebAPI no Docker e Solução

Ao tentar rodar o serviço WebAPI pela primeira vez dentro do Docker, enfrentei uma dificuldade importante: a aplicação encerrava imediatamente com exit code 0, sem logs claros de erro. Após debugar, identifiquei que o problema estava relacionado à configuração de HTTPS no ambiente Docker. O ASP.NET tentava configurar um endpoint HTTPS, mas não encontrava um certificado de desenvolvimento válido dentro do container, resultando na exceção:

```
System.InvalidOperationException: Unable to configure HTTPS endpoint. No server certificate was specified, and the default developer certificate could not be found or is out of date.
```

### Passos para resolver:
- Remover a linha `app.UseHttpsRedirection();` do `Program.cs` para não forçar redirecionamento HTTPS no Docker.
- Remover a variável de ambiente `ASPNETCORE_HTTPS_PORTS` do `docker-compose.yml` para evitar que o Kestrel tente subir um endpoint HTTPS.
- Garantir que a aplicação rode apenas em HTTP no ambiente Docker, utilizando apenas a porta 8080.
- Adicionar logs detalhados no bloco de exceção do `Program.cs` para capturar e exibir o erro real.

Essas ações permitiram que a aplicação subisse corretamente no Docker, facilitando o desenvolvimento e testes locais.

### Execução automática das migrations
Outra melhoria importante foi a configuração para que as migrations do Entity Framework Core sejam executadas automaticamente na inicialização do serviço. Isso garante que o banco de dados esteja sempre atualizado com o modelo da aplicação, sem necessidade de rodar comandos manuais.

Trecho de código adicionado ao `Program.cs`:

```csharp
// Executa as migrations automaticamente ao iniciar
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DefaultContext>();
    db.Database.Migrate();
}
```

Com isso, o ambiente Docker fica mais robusto e fácil de usar para novos desenvolvedores, reduzindo o tempo de setup e evitando erros comuns de configuração.

---------------

# Melhorias Recentes - 19/05

## Centralização da Regra de Desconto
- Foi criada a classe `DiscountCalculator` em `Domain/Common`, responsável por calcular o desconto aplicado em cada item da venda.
- Agora, tanto a criação quanto a atualização de vendas utilizam essa classe, garantindo consistência e facilitando a manutenção da regra de negócio.
- Exemplo de uso:
```csharp
var discount = DiscountCalculator.CalculateDiscount(quantity);
```
- **Benefício:** Qualquer alteração na regra de desconto é feita em um único lugar e reflete em toda a aplicação.

## Enriquecimento das Exceções de Validação
- A resposta de erro de validação foi aprimorada para retornar informações detalhadas sobre cada falha de validação.
- Agora, cada erro inclui:
  - `error`: Código do erro de validação
  - `detail`: Mensagem detalhada do erro
  - `propertyName`: Nome do campo que falhou
  - `attemptedValue`: Valor informado que causou o erro
  - `errorCode`: Código interno do tipo de erro
  - `severity`: Severidade do erro (ex: Error, Warning)
- Exemplo de resposta:
```json
{
  "success": false,
  "message": "Validation Failed",
  "errors": [
    {
      "error": "LessThanOrEqualValidator",
      "detail": "A quantidade deve estar entre 1 e 20 itens",
      "propertyName": "Items[0].Quantity",
      "attemptedValue": 25,
      "errorCode": "LessThanOrEqualValidator",
      "severity": "Error"
    }
  ]
}
```
- **Benefício:** Facilita o diagnóstico de erros pelo consumidor da API e melhora a experiência de integração.

---
