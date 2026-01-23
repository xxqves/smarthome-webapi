namespace SmHm.WebApi.Contracts.Auth
{
    public record RegisterResponse(
        Guid UserId,
        string Email,
        string UserName);
}
