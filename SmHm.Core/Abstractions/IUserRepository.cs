using SmHm.Core.Models;

namespace SmHm.Core.Abstractions;

public interface IUserRepository
{
    Task<Guid> Create(User user);
    Task<User> GetByEmail(string email);
}