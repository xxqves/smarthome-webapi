namespace SmHm.NotificationService.Interfaces
{
    public interface INotificationHandler
    {
        Task HandleAsync(string message, CancellationToken cancellationToken = default);
    }
}