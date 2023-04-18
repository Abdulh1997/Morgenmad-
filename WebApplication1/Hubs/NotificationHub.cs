using Microsoft.AspNetCore.SignalR;

namespace WebApplication1.Hubs
{
    public class NotificationHub : Hub<INotificationHub>
    {
        public async Task Update()
        {
            await Clients.All.Update();
        }
    }
}
