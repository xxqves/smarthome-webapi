namespace SmHm.Core.Abstractions
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }

        string UserName { get; }

        bool IsAuthenticated { get; }
    }
}
