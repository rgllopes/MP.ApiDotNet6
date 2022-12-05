using FluentValidation;
using MP.ApiDotNet6.Application.DTOs.User;

namespace MP.ApiDotNet6.Application.DTOs.Validations
{
    public class UserDTOValidator : AbstractValidator<UserDTO>
    {
        public UserDTOValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .NotEmpty()
                .WithMessage("Email deve ser informado!");

            RuleFor(x => x.Password)
                .NotEmpty()
                .NotEmpty()
                .WithMessage("Password deve ser informado!");
        }

    }
}
