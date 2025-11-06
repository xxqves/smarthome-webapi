using SmHm.Core.Abstractions;
using SmHm.Core.Abstractions.Auth;
using SmHm.Core.Models;

namespace SmHm.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;

        public UserService(IUserRepository repository, IPasswordHasher passwordHasher, IJwtProvider jwtProvider)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }

        public async Task<Guid> Register(string userName, string email, string password)
        {
            var hashedPassword = _passwordHasher.Generate(password);

            var user = User.Create(
                Guid.NewGuid(),
                userName,
                email,
                hashedPassword,
                new List<Room>());

            return await _repository.Create(user);
        }

        public async Task<string> Login(string email, string password)
        {
            var user = await _repository.GetByEmail(email);

            var result = _passwordHasher.Verify(password, user.PasswordHash);

            if (result == false)
            {
                throw new Exception("Failed to login");
            }

            var token = _jwtProvider.GenerateToken(user);

            return token;
        }
    }
}
