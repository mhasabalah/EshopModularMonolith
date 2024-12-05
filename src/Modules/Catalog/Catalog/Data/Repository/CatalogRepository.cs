using Shared.Pagination;

namespace Catalog.Data.Repository;

public class CatalogRepository(CatalogDbContext dbContext) : ICatalogRepository
{
    public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
    {
        await dbContext.Products.AddAsync(product, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> DeleteAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        Product product = await GetByIdAsync(productId, false, cancellationToken);
        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Products.AsNoTracking()
            .OrderBy(e => e.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<PaginatedResult<Product>> GetAllAsync(PaginationRequest request,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Product> query = dbContext.Products.AsQueryable();
        long totalItems = await query.LongCountAsync(cancellationToken);
        List<Product> items = await query
            .Skip(request.PageSize * request.PageIndex)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<Product>(request.PageIndex, request.PageSize, totalItems, items);
    }

    public async Task<Product> GetByIdAsync(Guid productId, bool asNoTracking = true,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Product> query = dbContext.Products.AsQueryable();
        if (asNoTracking)
            query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(p => p.Id == productId, cancellationToken) ??
               throw new ProductNotFoundException(productId);
    }

    public async Task UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        dbContext.Products.Update(product);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> SaveChangesAsync(string? userName = null, CancellationToken cancellationToken = default) =>
        await dbContext.SaveChangesAsync(cancellationToken);

    public async Task<IEnumerable<Product>> GetProductsByCategory(string category,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Products.Where(p => p.Category.Contains(category))
            .OrderBy(e => e.Name)
            .ToListAsync(cancellationToken);
    }
}