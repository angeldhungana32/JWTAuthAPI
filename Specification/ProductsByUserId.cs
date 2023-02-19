using JWTAuthAPI.Entities;

namespace JWTAuthAPI.Specification
{
    public class ProductsByUserId : BaseSpecification<Product>
    {
        public ProductsByUserId(Guid userId) : base(x => x.UserId == userId) { }
    }
}
