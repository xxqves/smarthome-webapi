namespace SmHm.Contracts.Events.UserEvents
{
    public record UserRegistered(
        Guid UserId,
        string Email,
        DateTime RegisteredAt);
}
