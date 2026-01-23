namespace SmHm.WebApi.Contracts.Auth
{
    public record LoginResponse(
        Guid UserId,
        string Token,
        string Email,
        string UserName);
}
