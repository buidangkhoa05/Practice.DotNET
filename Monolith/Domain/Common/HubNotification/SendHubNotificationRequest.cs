namespace Domain.Common.HubNotification
{
    public class SendHubNotificationRequest
    {
        public SendHubNotificationData Notification { get; set; } = null!;
        public int[] ReceiverIds { get; set; } = null!;
    }
}
