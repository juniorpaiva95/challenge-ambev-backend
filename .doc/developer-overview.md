# Developer Overview - 16/05/2025

## Business Rules Checklist & Implementation

Below are the business rules from the challenge, with a checkmark (✔️) for each rule that is implemented, an explanation, and a code snippet showing how it is enforced:

### ✔️ Purchases above 4 identical items have a 10% discount
### ✔️ Purchases between 10 and 20 identical items have a 20% discount
### ✔️ Purchases below 4 items cannot have a discount

**How it's implemented:**
The discount logic is handled in the `CreateSaleHandler` using the `CalculateDiscount` method:

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

This method is called for each sale item when creating a sale, ensuring the correct discount is applied according to the quantity.

---

### ✔️ It's not possible to sell above 20 identical items

**How it's implemented:**
This rule is enforced by validation using FluentValidation in `CreateSaleItemValidator`:

```csharp
RuleFor(x => x.Quantity)
    .GreaterThan(0)
    .WithMessage("A quantidade deve ser maior que zero.")
    .LessThanOrEqualTo(20)
    .WithMessage("A quantidade máxima por item é 20.");
```

If a request tries to add more than 20 units of the same product, the API will return a validation error and will not process the sale.

---

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
