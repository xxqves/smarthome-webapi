using FluentValidation;
using SmHm.WebApi.Contracts.Users;

namespace SmHm.WebApi.Contracts.Validators.Users
{
    public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
    {
        private const int MAX_USERNAME_LENGTH = 18;
        private const int MIN_PASSWORD_LENGTH = 6;
        private const int MAX_PASSWORD_LENGTH = 100;

        public RegisterUserRequestValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage($"'{nameof(RegisterUserRequest.UserName)}' is required.")
                .MaximumLength(MAX_USERNAME_LENGTH).WithMessage($"'{nameof(RegisterUserRequest.UserName)}' must not exceed {MAX_USERNAME_LENGTH} characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage($"'{nameof(RegisterUserRequest.Email)}' is required.")
                .EmailAddress().WithMessage($"'{nameof(RegisterUserRequest.Email)}' is not a valid email address.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage($"'{nameof(RegisterUserRequest.Password)}' is required.")
                .MinimumLength(MIN_PASSWORD_LENGTH).WithMessage($"'{nameof(RegisterUserRequest.Password)}' must be at least {MIN_PASSWORD_LENGTH} characters.")
                .MaximumLength(MAX_PASSWORD_LENGTH).WithMessage($"'{nameof(RegisterUserRequest.Password)}' must not exceed {MAX_PASSWORD_LENGTH} characters.");
        }
    }
}
