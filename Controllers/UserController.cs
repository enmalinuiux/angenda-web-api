using Microsoft.AspNetCore.Mvc;
using agenda_web_api.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace agenda_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private agendaContext _context;

        public UserController(agendaContext context)
        {
            _context = context;
        }

        // method: GET/
        [HttpGet]
        public IActionResult Get()
        {
            var users = _context.User.ToList();
        
            return Ok(_context.User.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var user = _context.User.Find(id);
            
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Post(User user)
        {
            _context.User.Add(user);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var user = _context.User.Where(x => x.Id == id).FirstOrDefault();
            _context.User.Remove(user);
            _context.SaveChanges();

            return Ok();
        }
    }
}