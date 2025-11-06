using SmHm.Core.Models;

namespace SmHm.Core.Abstractions.Auth
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}