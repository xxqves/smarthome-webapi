namespace SmHm.WebApi.Contracts.Users
{
    public record LoginUserRequest(
        string Email,
        string Password);
}
