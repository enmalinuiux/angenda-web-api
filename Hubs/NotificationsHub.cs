using System.Threading.Tasks;
using agenda_web_api.Models.DTO;
using Microsoft.AspNetCore.SignalR;

namespace agenda_web_api.Hubs
{
    public class NotificationsHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public Task Notify(NotificationDTO notification){
            return Clients.Client(notification.ContactId).SendAsync("signalr",notification);
        }
    }
}