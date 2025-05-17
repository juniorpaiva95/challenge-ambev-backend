using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly DefaultContext _context;

    public SaleRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        await _context.Sales.AddAsync(sale, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return sale;
    }

    public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Sales
            .Include(s => s.Items)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<bool> UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        _context.Sales.Update(sale);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var sale = await GetByIdAsync(id, cancellationToken);
        if (sale == null)
            return false;
        _context.Sales.Remove(sale);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public Task<Sale?> GetBySaleNumberAsync(string saleNumber, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<(IEnumerable<Sale> Sales, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize, Guid? customerId = null, Guid? productId = null, CancellationToken cancellationToken = default)
    {
        var query = _context.Sales
            .Include(s => s.Customer)
            .Include(s => s.Items)
                .ThenInclude(i => i.Product)
            .AsQueryable();

        if (customerId.HasValue)
            query = query.Where(s => s.CustomerId == customerId.Value);
        if (productId.HasValue)
            query = query.Where(s => s.Items.Any(i => i.ProductId == productId.Value));

        var totalCount = await query.CountAsync(cancellationToken);
        var sales = await query
            .OrderByDescending(s => s.SaleDate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        return (sales, totalCount);
    }

    public async Task<SaleItem?> GetItemByIdAsync(Guid itemId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<SaleItem>()
            .Include(i => i.Product)
            .FirstOrDefaultAsync(i => i.Id == itemId, cancellationToken);
    }

    public async Task<bool> UpdateItemAsync(SaleItem item, CancellationToken cancellationToken = default)
    {
        _context.Set<SaleItem>().Update(item);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
} 