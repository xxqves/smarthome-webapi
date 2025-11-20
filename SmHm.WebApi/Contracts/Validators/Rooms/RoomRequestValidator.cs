using FluentValidation;
using SmHm.WebApi.Contracts.Rooms;

namespace SmHm.WebApi.Contracts.Validators.Rooms
{
    public class RoomRequestValidator : AbstractValidator<RoomRequest>
    {
        public const int MAX_NAME_LENGTH = 50;
        public const int MAX_DESCRIPTION_LENGTH = 150;

        public RoomRequestValidator()
        {
             
        }
    }
}
