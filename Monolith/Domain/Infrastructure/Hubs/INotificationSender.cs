using Domain.Common.HubNotification;

namespace Domain.Infrastructure.Hubs
{
    public interface INotificationSender
    {
        Task SendToAllAsync(SendHubNotificationRequest request);
        Task SendToUserAsync(SendHubNotificationRequest request);
    }
}