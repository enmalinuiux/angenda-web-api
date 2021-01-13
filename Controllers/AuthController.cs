using Microsoft.AspNetCore.Mvc;
using agenda_web_api.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using agenda_web_api.Tools;
using agenda_web_api.Services;
using agenda_web_api.Models.DTO;

namespace agenda_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly agendaContext _context;
        private IUserService _userService;

        public AuthController(agendaContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        // Method: POST/
        [HttpPost("signup")]
        public async Task<ActionResult<UserDTO>> SignUp(User user)
        {
            user.Id = Encryptor.CreateUUID();
            user.Pass = Encryptor.GetSHA256(user.Pass);

            await _context.User.AddAsync(user);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                return BadRequest(e.InnerException);
            }

            return CreatedAtAction(nameof(UserController.Get), new { id = user.Id }, user);
        }

        // Method: POST/authenticate/
        [HttpPost("signin")]
        public IActionResult Authenticate(AuthenticateRequestDTO model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
    }
}