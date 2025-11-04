namespace SmHm.Core.Models
{
    public class User
    {
        public const int MAX_USERNAME_LENGTH = 12;

        private User(Guid id, string userName, string email, string passwordHash)
        {
            Id = id;
            UserName = userName;
            Email = email;
            PasswordHash = passwordHash;
        }

        public Guid Id { get; }

        public string UserName { get; } = string.Empty;

        public string Email { get; } = string.Empty;

        public string PasswordHash { get; } = string.Empty;

        public static User Create(Guid id, string userName, string email, string passwordHash)
        {
            if (userName.Length > MAX_USERNAME_LENGTH)
            {
                throw new ArgumentException("Name length can not be more than 12 characters");
            }

            return new User(id, userName, email, passwordHash);
        }
    }
}
