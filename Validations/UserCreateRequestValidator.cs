using FluentValidation;
using JWTAuthAPI.Entities.DTOs.Authentication;
using JWTAuthAPI.Entities.DTOs.UserAccount;

namespace JWTAuthAPI.Validations
{
    public class UserCreateRequestValidator : AbstractValidator<UserCreateRequest>
    {
        public UserCreateRequestValidator() 
        {
            RuleFor(v => v.Password)
               .NotNull()
               .NotEmpty()
               .MinimumLength(8);

            RuleFor(v => v.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(300);

            RuleFor(v => v.FirstName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(v => v.LastName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(30);
        }
    }
}
