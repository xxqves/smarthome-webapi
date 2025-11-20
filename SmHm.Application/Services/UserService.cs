using SmHm.Contracts.Events.UserEvents;
using SmHm.Core.Abstractions;
using SmHm.Core.Abstractions.Auth;
using SmHm.Core.Abstractions.Messaging;
using SmHm.Core.Models;

namespace SmHm.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        private readonly IMessageBus _messageBus;

        public UserService(IUserRepository repository, ICurrentUserService currentUserService, IPasswordHasher passwordHasher, IJwtProvider jwtProvider, IMessageBus messageBus)
        {
            _repository = repository;
            _currentUserService = currentUserService;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
            _messageBus = messageBus;
        }

        public async Task<Guid> Register(string userName, string email, string password, CancellationToken cancellationToken = default)
        {
            var hashedPassword = _passwordHasher.Generate(password);

            var user = User.Create(
                Guid.NewGuid(),
                userName,
                email,
                hashedPassword,
                new List<Room>());

            var userId = await _repository.Create(user, cancellationToken);

            var @event = new UserRegistered(user.Id, user.Email, DateTime.UtcNow);

            await _messageBus.PublishAsync(@event, cancellationToken);

            return userId;
        }
                
        public async Task<string> Login(string email, string password, CancellationToken cancellationToken = default)
        {
            if (_currentUserService.IsAuthenticated)
            {
                throw new Exception("You have already logged in.");
            }

            var user = await _repository.GetByEmail(email, cancellationToken);

            var result = _passwordHasher.Verify(password, user.PasswordHash);

            if (result == false)
            {
                throw new Exception("Failed to login. Email or Password is wrong.");
            }

            var token = _jwtProvider.GenerateToken(user);

            var @event = new UserLoggedIn(user.Id, user.UserName, token, DateTime.UtcNow);

            await _messageBus.PublishAsync(@event, cancellationToken);

            return token;
        }
    }
}
