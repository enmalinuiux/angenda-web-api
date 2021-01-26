using Microsoft.AspNetCore.Mvc;
using agenda_web_api.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using agenda_web_api.Hubs;
using agenda_web_api.Services;
using agenda_web_api.Tools;
using Microsoft.AspNetCore.SignalR;
using agenda_web_api.Models.DTO;

namespace agenda_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly agendaContext _context;
        private readonly IHubContext<NotificationsHub> _hubContext;
        public NotificationController(agendaContext context, IUserService userService, IHubContext<NotificationsHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        //Method: POST/
        [HttpPost]
        public async Task<ActionResult> SendNotification(Notification notification)
        {
            notification.Id = Encryptor.CreateUUID();

            await _hubContext.Clients.Client(notification.ContactId).SendAsync("signalr",notification);
            await _context.Notification.AddAsync(notification);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException);
            }

            return Ok(  new { res = "Successfully!!" } );
        }
    }
}