
namespace Catalog.Products.Features.DeleteProduct;

public record DeleteProductCommand(Guid ProductId)
    : ICommand<DeleteProductResult>;

public record DeleteProductResult(bool IsSuccess);

public class DeleteProductHandler(IProductRepository _productRepository) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        bool response = await _productRepository.DeleteAsync(command.ProductId, cancellationToken);

        return new DeleteProductResult(response);
    }
}
