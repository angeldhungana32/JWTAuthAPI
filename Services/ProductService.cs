using JWTAuthAPI.Authorization;
using JWTAuthAPI.Entities;
using JWTAuthAPI.Entities.Identity;
using JWTAuthAPI.Helpers;
using JWTAuthAPI.Interfaces;
using JWTAuthAPI.Specification;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace JWTAuthAPI.Services
{
    public class ProductService : IProductService
    {

        private readonly IRepositoryActivator _repositoryActivator;
        private readonly IAccountService _accountService;

        public ProductService(IRepositoryActivator repositoryActivator, IAccountService accountService)
        {
            _repositoryActivator = repositoryActivator;
            _accountService = accountService;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            return await _repositoryActivator
                .Repository<Product>()
                .AddAsync(product);
        }

        public async Task<bool> DeleteProductAsync(Product product)
        {
            return await _repositoryActivator
                    .Repository<Product>()
                    .DeleteAsync(product);
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

        public async Task<bool> UpdateProductAsync(Product product)
        {
            return await _repositoryActivator
                .Repository<Product>()
                .UpdateAsync(product);
        }

        public async Task<bool> AuthorizeProductOwnerAsync(ClaimsPrincipal userContext, Product resource)
        {
            var user = await _accountService.GetUserByIdAsync(resource.UserId.ToString());

            if(user == null) { return false; }

            return await _accountService.AuthorizeOwnerAsync(userContext, user);
        }
    }
}
