# Developer Overview - 16/05/2025

## Fluxo de Mapeamento (DTOs e Entidades)

O projeto segue um padrão limpo e desacoplado para o fluxo de dados entre as camadas, utilizando o AutoMapper para conversão entre objetos. O fluxo é o seguinte:

```
Request (WebApi) -> Command (Application) -> Entity (Domain) -> Result (Application) -> Response (WebApi)
```

### Exemplo do fluxo para o caso de uso de Sale:

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
