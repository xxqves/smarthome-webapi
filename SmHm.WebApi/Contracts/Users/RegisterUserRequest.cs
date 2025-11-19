namespace SmHm.WebApi.Contracts.Users
{
    public record RegisterUserRequest(
        string UserName,
        string Email,
        string Password);
}
