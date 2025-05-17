using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISaleRepository
{
    Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default);
    Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Sale?> GetBySaleNumberAsync(string saleNumber, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Sale sale, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<(IEnumerable<Sale> Sales, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize, Guid? customerId = null, Guid? productId = null, CancellationToken cancellationToken = default);
    Task<SaleItem?> GetItemByIdAsync(Guid itemId, CancellationToken cancellationToken = default);
    Task<bool> UpdateItemAsync(SaleItem item, CancellationToken cancellationToken = default);
}