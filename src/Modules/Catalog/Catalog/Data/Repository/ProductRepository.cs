namespace Catalog.Data.Repository;
public class ProductRepository(CatalogDbContext _dbContext) : IProductRepository
{

    public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _dbContext.Products.AddAsync(product, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> DeleteAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        var product = await GetByIdAsync(productId, false, cancellationToken);
        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _dbContext.Products.AsNoTracking()
                                 .OrderBy(e => e.Name)
                                 .ToListAsync(cancellationToken);

    public async Task<Product> GetByIdAsync(Guid productId, bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Products.AsQueryable();
        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }
        return await query.FirstOrDefaultAsync(p => p.Id == productId, cancellationToken) ?? throw new ArgumentNullException();

    }

    public async Task UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        _dbContext.Products.Update(product);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> SaveChangesAsync(string? userName = null, CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetProductsByCategory(string category, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Products.Where(p => p.Category.Contains(category))
                                        .OrderBy(e => e.Name)
                                        .ToListAsync(cancellationToken);
    }
}
