using Microsoft.AspNetCore.Mvc;
using agenda_web_api.Models;

namespace agenda_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // method: GET/
        [HttpGet]
        public IActionResult Get(){
            using (agendaContext db = new agendaContext())
            {
                return  Ok(db.User.Find("9b1deb4d-3b7d-4bad-9bdd-2b0d7b3dcb6d"));
            }
        }
    }
}