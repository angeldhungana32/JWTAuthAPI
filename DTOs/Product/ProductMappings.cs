using JWTAuthAPI.Helpers;

namespace JWTAuthAPI.DTOs.Product
{
    public static class ProductMappings
    {
        public static ProductResponse ToResponseDTO(this Entities.Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));

            return new ProductResponse()
            {
                Id = product.Id.ToString(),
                Description = product.Description,
                Price = product.Price,
                ProductName = product.Name,
                Quantity = product.Quantity,
                UserId = product.UserId.ToString()
            };
        }

        public static Entities.Product ToEntity(this ProductCreateRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            return new Entities.Product()
            {
                 Description = request.Description,
                 Name = request.Description,
                 Price = request.Price,
                 Quantity = request.Quantity,
                 UserId = GuidParser.Parse(request.UserId),
            };
        }
    }
}
