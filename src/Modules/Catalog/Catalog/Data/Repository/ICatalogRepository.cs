using Shared.Pagination;

namespace Catalog.Data.Repository;

public interface ICatalogRepository
{
    Task AddAsync(Product product, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid productId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<PaginatedResult<Product>> GetAllAsync(PaginationRequest request,
        CancellationToken cancellationToken = default);

    Task<Product> GetByIdAsync(Guid productId, bool asNoTracking = true, CancellationToken cancellationToken = default);
    Task<IEnumerable<Product>> GetProductsByCategory(string category, CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(string? userName = null, CancellationToken cancellationToken = default);
    Task UpdateAsync(Product product, CancellationToken cancellationToken = default);
}