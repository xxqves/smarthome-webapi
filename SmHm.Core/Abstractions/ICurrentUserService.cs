namespace SmHm.Core.Abstractions
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }

        bool IsAuthenticated { get; }
    }
}
