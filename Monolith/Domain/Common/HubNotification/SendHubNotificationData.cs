namespace Domain.Common.HubNotification
{
    public class SendHubNotificationData
    {
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string? Payload { get; set; } = null;
    }
}
