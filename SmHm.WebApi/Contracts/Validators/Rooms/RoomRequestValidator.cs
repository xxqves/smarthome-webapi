using FluentValidation;
using SmHm.WebApi.Contracts.Devices;
using SmHm.WebApi.Contracts.Rooms;

namespace SmHm.WebApi.Contracts.Validators.Rooms
{
    public class RoomRequestValidator : AbstractValidator<RoomRequest>
    {
        private const int MAX_NAME_LENGTH = 50;
        private const int MAX_DESCRIPTION_LENGTH = 150;
        private const int MIN_FLOOR_ARGUMENT = 0;
        private const int MAX_FLOOR_ARGUMENT = 100;

        public RoomRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage($"'{nameof(RoomRequest.Name)}' is required.")
                .MaximumLength(MAX_NAME_LENGTH).WithMessage($"'{nameof(RoomRequest.Name)}' must not exceed {MAX_NAME_LENGTH} characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage($"'{nameof(RoomRequest.Description)}' is required.")
                .MaximumLength(MAX_DESCRIPTION_LENGTH).WithMessage($"'{nameof(RoomRequest.Description)}' must not exceed {MAX_DESCRIPTION_LENGTH} characters.");

            RuleFor(x => x.RoomType)
                .IsInEnum().WithMessage($"'{nameof(RoomRequest.RoomType)}' has invalid value.");

            RuleFor(x => x.Floor)
                .NotEmpty().WithMessage($"'{nameof(RoomRequest.Floor)}' is required.")
                .ExclusiveBetween(MIN_FLOOR_ARGUMENT, MAX_FLOOR_ARGUMENT).WithMessage($"'{nameof(RoomRequest.Floor)}' the value must be in range from {MIN_FLOOR_ARGUMENT} to {MAX_FLOOR_ARGUMENT}.");
        }
    }
}
