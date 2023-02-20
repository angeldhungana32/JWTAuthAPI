using FluentValidation;
using JWTAuthAPI.Entities.DTOs.Product;

namespace JWTAuthAPI.Validations
{
    public class ProductCreateRequestValidator : AbstractValidator<ProductCreateRequest>
    {
        public ProductCreateRequestValidator() 
        {
            RuleFor(v => v.Name)
                 .NotNull()
                 .NotEmpty()
                 .MinimumLength(100);

            RuleFor(v => v.Price)
                .GreaterThanOrEqualTo(0);

            RuleFor(v => v.Description)
                .NotNull()
                .NotEmpty()
                .MaximumLength(2000);

            RuleFor(v => v.UserId)
                .NotNull()
                .NotEmpty();

        }
    }
}
