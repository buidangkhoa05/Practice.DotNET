using System.Security.Claims;

namespace Infrastructure.Hubs.Notification
{
    [Authorize]
    public class NotificationHub : Hub
    {
        public NotificationHub()
        {
        }

        public override async Task OnConnectedAsync()
        {
            var (connectionId, userId) = GetConnectionIdAndUserId();
            await Groups.AddToGroupAsync(connectionId, userId);
            await Console.Out.WriteLineAsync($"ConnectionId: {connectionId} connected, UserId: {userId}");
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var (connectionId, userId) = GetConnectionIdAndUserId();
            await Groups.RemoveFromGroupAsync(connectionId, userId);
            await Console.Out.WriteLineAsync($"ConnectionId: {connectionId} disconnected, UserId: {userId}");
        }
        /// <summary>
        /// Get connectionId and userId
        /// </summary>
        /// <returns></returns>

        private (string, string) GetConnectionIdAndUserId()
        {
            var connectionId = Context.ConnectionId;
            var userId = Context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            return (connectionId, userId);
        }
    }
}
