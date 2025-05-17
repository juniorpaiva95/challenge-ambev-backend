# Visão Geral do Desenvolvedor - 16/05/2025

## Checklist de Regras de Negócio & Implementação

Abaixo estão as regras de negócio do desafio, com um check (✔️) para cada regra implementada, uma explicação e um trecho de código mostrando como ela é aplicada:

### ✔️ Compras acima de 4 itens idênticos têm 10% de desconto
### ✔️ Compras entre 10 e 20 itens idênticos têm 20% de desconto
### ✔️ Compras abaixo de 4 itens não podem ter desconto

**Como está implementado:**
A lógica de desconto é tratada no `CreateSaleHandler` usando o método `CalculateDiscount`:

```csharp
private decimal CalculateDiscount(int quantity)
{
    if (quantity < 4)
        return 0m;
    if (quantity >= 10 && quantity <= 20)
        return 0.20m;
    if (quantity >= 4)
        return 0.10m;
    return 0m;
}
```

Esse método é chamado para cada item da venda ao criar uma venda, garantindo que o desconto correto seja aplicado de acordo com a quantidade.

---

### ✔️ Não é possível vender mais de 20 itens idênticos

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
