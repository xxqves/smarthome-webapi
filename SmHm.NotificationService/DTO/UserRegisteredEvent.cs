namespace SmHm.NotificationService.DTO
{
    public record UserRegisteredEvent(
        Guid UserId,
        string Email,
        DateTime RegisteredAt);
}
