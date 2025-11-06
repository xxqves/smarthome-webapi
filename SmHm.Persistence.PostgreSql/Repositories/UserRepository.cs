using Microsoft.EntityFrameworkCore;
using SmHm.Core.Abstractions;
using SmHm.Core.Models;
using SmHm.Persistence.PostgreSql.Entities;

namespace SmHm.Persistence.PostgreSql.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SmartHomeDbContext _context;

        public UserRepository(SmartHomeDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Create(User user)
        {
            var userEntity = new UserEntity
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                Rooms = new List<RoomEntity>()
            };

            await _context.AddAsync(userEntity);
            await _context.SaveChangesAsync();

            return userEntity.Id;
        }

        public async Task<User> GetByEmail(string email)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email) ?? throw new Exception("Not found");

            var user = User.Create(
                userEntity!.Id,
                userEntity.UserName,
                userEntity.Email,
                userEntity.PasswordHash,
                new List<Room>());

            return user;
        }
    }
}
