
namespace SmHm.Core.Abstractions;

public interface IAuthService
{
    Task<string> Login(string email, string password, CancellationToken cancellationToken = default);
    Task<Guid> Register(string userName, string email, string password, CancellationToken cancellationToken = default);
}