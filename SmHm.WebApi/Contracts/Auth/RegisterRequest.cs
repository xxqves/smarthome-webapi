namespace SmHm.WebApi.Contracts.Users
{
    public record RegisterRequest(
        string UserName,
        string Email,
        string Password);
}
