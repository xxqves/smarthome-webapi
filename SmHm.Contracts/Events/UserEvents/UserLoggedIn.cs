namespace SmHm.Contracts.Events.UserEvents
{
    public record UserLoggedIn(
        Guid UserId,
        string UserName,
        string UserToken,
        DateTime LoggedAt);
}
