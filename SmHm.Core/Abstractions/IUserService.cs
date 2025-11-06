
namespace SmHm.Core.Abstractions;

public interface IUserService
{
    Task<string> Login(string email, string password, CancellationToken cancellationToken = default);
    Task<Guid> Register(string userName, string email, string password, CancellationToken cancellationToken = default);
}