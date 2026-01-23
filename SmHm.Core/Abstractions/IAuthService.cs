
namespace SmHm.Core.Abstractions;

public interface IAuthService
{
    Task<(Guid userId, string token, string userName)> Login(string email, string password, CancellationToken cancellationToken = default);
    Task<Guid> Register(string userName, string email, string password, CancellationToken cancellationToken = default);
}