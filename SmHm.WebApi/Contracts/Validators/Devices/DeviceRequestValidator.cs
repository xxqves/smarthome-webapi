using FluentValidation;
using SmHm.WebApi.Contracts.Devices;

namespace SmHm.WebApi.Contracts.Validators.Devices
{
    public class DeviceRequestValidator : AbstractValidator<DeviceRequest>
    {
        private const int MAX_NAME_LENGTH = 50;
        private const int MAX_DESCRIPTION_LENGTH = 150;

        public DeviceRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage($"'{nameof(DeviceRequest.Name)}' is required.")
                .MaximumLength(MAX_NAME_LENGTH).WithMessage($"'{nameof(DeviceRequest.Name)}' must not exceed {MAX_NAME_LENGTH} characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage($"'{nameof(DeviceRequest.Description)}' is required.")
                .MaximumLength(MAX_DESCRIPTION_LENGTH).WithMessage($"'{nameof(DeviceRequest.Description)}' must not exceed {MAX_DESCRIPTION_LENGTH} characters.");

            RuleFor(x => x.DeviceType)
                .NotEmpty().WithMessage($"'{nameof(DeviceRequest.DeviceType)}' is required.");

            RuleFor(x => x.RoomId)
                .NotEmpty().WithMessage($"'{nameof(DeviceRequest.RoomId)}' is required.");
        }
    }
}
