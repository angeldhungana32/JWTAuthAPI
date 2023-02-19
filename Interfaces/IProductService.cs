using JWTAuthAPI.Entities;

namespace JWTAuthAPI.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetProductByIdAsync(string id, 
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Product>> ListAllProductsByUserIdAsync(string id, 
            CancellationToken cancellationToken = default);
        Task<Product> AddProductAsync(Product entity, 
            CancellationToken cancellationToken = default);
        Task UpdateProductAsync(Product entity, 
            CancellationToken cancellationToken = default);
        Task DeleteProductAsync(string id, 
            CancellationToken cancellationToken = default);
    }
}
