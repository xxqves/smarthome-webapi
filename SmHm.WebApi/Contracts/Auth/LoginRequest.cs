namespace SmHm.WebApi.Contracts.Users
{
    public record LoginRequest(
        string Email,
        string Password);
}
