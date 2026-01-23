using FluentValidation;
using SmHm.WebApi.Contracts.Users;

namespace SmHm.WebApi.Contracts.Validators.Users
{
    public class RegisterUserRequestValidator : AbstractValidator<RegisterRequest>
    {
        private const int MAX_USERNAME_LENGTH = 18;
        private const int MIN_PASSWORD_LENGTH = 6;
        private const int MAX_PASSWORD_LENGTH = 100;

        public RegisterUserRequestValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage($"'{nameof(RegisterRequest.UserName)}' is required.")
                .MaximumLength(MAX_USERNAME_LENGTH).WithMessage($"'{nameof(RegisterRequest.UserName)}' must not exceed {MAX_USERNAME_LENGTH} characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage($"'{nameof(RegisterRequest.Email)}' is required.")
                .EmailAddress().WithMessage($"'{nameof(RegisterRequest.Email)}' is not a valid email address.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage($"'{nameof(RegisterRequest.Password)}' is required.")
                .MinimumLength(MIN_PASSWORD_LENGTH).WithMessage($"'{nameof(RegisterRequest.Password)}' must be at least {MIN_PASSWORD_LENGTH} characters.")
                .MaximumLength(MAX_PASSWORD_LENGTH).WithMessage($"'{nameof(RegisterRequest.Password)}' must not exceed {MAX_PASSWORD_LENGTH} characters.");
        }
    }
}
