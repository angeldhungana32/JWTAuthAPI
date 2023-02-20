using JWTAuthAPI.Entities;
using JWTAuthAPI.Helpers;
using JWTAuthAPI.Interfaces;
using JWTAuthAPI.Specification;

namespace JWTAuthAPI.Services
{
    public class ProductService : IProductService
    {

        private readonly IRepositoryActivator _repositoryActivator;

        public ProductService(IRepositoryActivator repositoryActivator)
        {
            _repositoryActivator = repositoryActivator;
        }

        public async Task<Product> AddProductAsync(Product entity)
        {
            return await _repositoryActivator
                .Repository<Product>()
                .AddAsync(entity);
        }

        public async Task<bool> DeleteProductAsync(string id)
        {
            var entity = await GetProductByIdAsync(id);
            if(entity != null)
            {
                return await _repositoryActivator
                    .Repository<Product>()
                    .DeleteAsync(entity);
            }
            return false;
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            Guid guid = GuidParser.Parse(id);
            return await _repositoryActivator
                .Repository<Product>()
                .GetByIdAsync(guid);
        }

        public async Task<IReadOnlyList<Product>> ListAllProductsByUserIdAsync(string id)
        {
            Guid guid = GuidParser.Parse(id);
            return await _repositoryActivator
                .Repository<Product>()
                .ListAllAsync(new ProductsByUserId(guid));
        }

        public async Task<bool> UpdateProductAsync(Product entity)
        {
            return await _repositoryActivator
                .Repository<Product>()
                .UpdateAsync(entity);
        }
    }
}
