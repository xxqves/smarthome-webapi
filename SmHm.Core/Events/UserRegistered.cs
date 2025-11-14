namespace SmHm.Core.Events
{
    public record UserRegistered(
        Guid UserId,
        string Email,
        DateTime RegisteredAt);
}
