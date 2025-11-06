namespace SmHm.WebApi.Contracts
{
    public record LoginUserRequest(
        string Email,
        string Password);
}
