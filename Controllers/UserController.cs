using Microsoft.AspNetCore.Mvc;
using agenda_web_api.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using agenda_web_api.Tools;

namespace agenda_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly agendaContext _context;

        public UserController(agendaContext context)
        {
            _context = context;
        }

        // Method: GET/
        [HttpGet]
        public async Task<ActionResult<User>> Get()
        {
            var users = await _context.User.ToListAsync();
        
            return Ok(users);
        }

        // Method: GET/:id
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(string id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null) return NotFound();
            
            return Ok(user);
        }

        // Method: POST/
        [HttpPost]
        public async Task<ActionResult<User>> Post(User user)
        {
            user.Id = Encryptor.createUUID();
            user.Pass = Encryptor.GetSHA256(user.Pass);
            
            await _context.User.AddAsync(user);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException e)
            {
                Console.WriteLine(e.Message);

                return BadRequest();
            }

            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        // Method: PUT/:id
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, User user)
        {
            if(id != user.Id) return BadRequest();

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id)) return NotFound();
                
                throw;
            }
            catch(DbUpdateException e)
            {
                Console.WriteLine(e.Message);

                //return Ok("ERROR!");
            }

            return NoContent();
        }

        // Method: DELETE/:id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _context.User.Where(u => u.Id == id).FirstOrDefaultAsync();

            if(user == null) return NotFound();

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(string id) =>
            _context.User.Any(u => u.Id == id);
    }
}