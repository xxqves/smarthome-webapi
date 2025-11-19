using FluentValidation;
using SmHm.WebApi.Contracts.Users;

namespace SmHm.WebApi.Contracts.Validators.Users
{
    public class LoginUserRequestValidator : AbstractValidator<LoginUserRequest>
    {
        public LoginUserRequestValidator()
        {
            RuleFor(x => x.Email)
               .NotEmpty().WithMessage($"'{nameof(LoginUserRequest.Email)}' is required.")
               .EmailAddress().WithMessage($"'{nameof(LoginUserRequest.Email)}' is not a valid email address.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage($"'{nameof(LoginUserRequest.Password)} is required.'");
        }
    }
}
