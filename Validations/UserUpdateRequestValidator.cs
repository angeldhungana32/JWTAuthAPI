using FluentValidation;
using JWTAuthAPI.Entities.DTOs.UserAccount;

namespace JWTAuthAPI.Validations
{
    public class UserUpdateRequestValidator : AbstractValidator<UserUpdateRequest>
    {
        public UserUpdateRequestValidator() 
        {
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
