using Microsoft.AspNetCore.Mvc;
using agenda_web_api.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using agenda_web_api.Tools;
using agenda_web_api.Services;
using agenda_web_api.Models.HttpMethods;
using Microsoft.AspNetCore.Authorization;

namespace agenda_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly agendaContext _context;
        private IUserService _userService;

        public UserController(agendaContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        // Method: GET/
        [HttpGet]
        public async Task<ActionResult<User>> Get()
        {
            var users = await _context.User.ToListAsync();
        
            return Ok(users);
        }

        // Method: GET/:id
        [Authorize]
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
            //Console.WriteLine("El usuario es: ", user.Email);

            user.Id = Encryptor.CreateUUID();
            user.Pass = Encryptor.GetSHA256(user.Pass);

            await _context.User.AddAsync(user);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e.Message);

                return BadRequest();
            }
            
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        // Method: PUT/:id
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, User user)
        {
            //if (id != user.Id) return BadRequest(); 
            var existingUser = await _context.User.Where(u => u.Id == id).FirstOrDefaultAsync();

            try
            {
                if (existingUser != null)
                {
                    existingUser.Name = user.Name;
                    existingUser.LastName = user.LastName;
                    existingUser.Email = user.Email;
                    existingUser.Pass = user.Pass;
                    existingUser.Birth = user.Birth;
                    existingUser.Business = user.Business;
                    existingUser.UserType = user.UserType;
                    existingUser.AddressStreet = user.AddressStreet;
                    existingUser.AddressCity = user.AddressCity;
                    existingUser.AddressCountry = user.AddressCountry;

                    _context.Entry(existingUser).State = EntityState.Modified;

                    await _context.SaveChangesAsync();
                }
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
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _context.User.Where(u => u.Id == id).FirstOrDefaultAsync();

            if(user == null) return NotFound();

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Method: POST/authenticate/
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        private bool UserExists(string id) =>
            _context.User.Any(u => u.Id == id);
    }
}