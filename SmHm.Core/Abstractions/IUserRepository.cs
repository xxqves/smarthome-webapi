using SmHm.Core.Models;

namespace SmHm.Core.Abstractions;

public interface IUserRepository
{
    Task<Guid> Create(User user, CancellationToken cancellationToken = default);
    Task<User> GetByEmail(string email, CancellationToken cancellationToken = default);
}