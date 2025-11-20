namespace SmHm.Core.Models
{
    public sealed class User
    {
        public const int MAX_USERNAME_LENGTH = 18;

        private User(Guid id, string userName, string email, string passwordHash, List<Room> rooms)
        {
            Id = id;
            UserName = userName;
            Email = email;
            PasswordHash = passwordHash;
            Rooms = rooms;
        }

        public Guid Id { get; }

        public string UserName { get; } = string.Empty;

        public string Email { get; } = string.Empty;

        public string PasswordHash { get; } = string.Empty;

        public List<Room> Rooms { get; } = [];

        public static User Create(Guid id, string userName, string email, string passwordHash, List<Room> rooms)
        {
            if (userName.Length > MAX_USERNAME_LENGTH)
            {
                throw new ArgumentException($"User Name must not exceed {MAX_USERNAME_LENGTH} characters.");
            }

            return new User(id, userName, email, passwordHash, rooms);
        }
    }
}
