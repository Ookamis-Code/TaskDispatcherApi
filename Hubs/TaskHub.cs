using Microsoft.AspNetCore.SignalR;

namespace TaskDispatcherApi.Hubs
{
    public class TaskHub : Hub
    {
        public async Task SendNotification(string message)
        {
            await Clients.All.SendAsync("Notification Received", message);
        }
    }
}