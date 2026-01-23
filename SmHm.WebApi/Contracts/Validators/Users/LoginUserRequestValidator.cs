using FluentValidation;
using SmHm.WebApi.Contracts.Users;

namespace SmHm.WebApi.Contracts.Validators.Users
{
    public class LoginUserRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginUserRequestValidator()
        {
            RuleFor(x => x.Email)
               .NotEmpty().WithMessage($"'{nameof(LoginRequest.Email)}' is required.")
               .EmailAddress().WithMessage($"'{nameof(LoginRequest.Email)}' is not a valid email address.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage($"'{nameof(LoginRequest.Password)} is required.'");
        }
    }
}
