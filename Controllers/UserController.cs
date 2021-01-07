using Microsoft.AspNetCore.Mvc;
using agenda_web_api.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using agenda_web_api.Tools;
using agenda_web_api.Services;
using agenda_web_api.Models.HttpMethods;
using agenda_web_api.DTO;

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
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDTO>> Get()
        {
            var us = await _context.User.ToListAsync();

            var users = us.Select(u => new UserDTO{
                Id = u.Id,
                Name = u.Name,
                LastName = u.LastName,
                Email = u.Email,
                Birth = u.Birth,
                Business = u.Business,
                UserType = u.UserType,
                AddressStreet = u.AddressStreet,
                AddressCity = u.AddressCity,
                AddressCountry = u.AddressCountry,
            });

            return Ok(users);
        }

        // Method: GET/business-users
        [HttpGet("business-users")]
        public async Task<ActionResult<User>> GetBusinessUsers()
        {
            var users = await _context.User.Where(u => u.UserType == 1).ToListAsync();

            return Ok(users);
        }

        // Method: GET/:id
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> Get(string id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null) return NotFound();

            var userDTO = new UserDTO{
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                Birth = user.Birth,
                Business = user.Business,
                UserType = user.UserType,
                AddressStreet = user.AddressStreet,
                AddressCity = user.AddressCity,
                AddressCountry = user.AddressCountry,
            };

            return Ok(userDTO);
        }

        // Method: GET/:id/contacts/
        [HttpGet("{id}/contacts")]
        public async Task<ActionResult<ContactDTO>> GetContacts(string id)
        {
            var Usercontacts = await _context.UserContact.Include(c => c.Contact).Include(p =>p.User.UserPhone).Where(u => u.UserId == id).ToListAsync();            

            var contactList = Usercontacts.Select(contacts => new ContactDTO
            {
                Id = contacts.ContactId,
                Name = contacts.Contact.Name,
                LastName = contacts.Contact.LastName,
                Email = contacts.Contact.Email,
                Birth = contacts.Contact.Birth,
                UserType = contacts.Contact.UserType,
                AddressStreet = contacts.Contact.AddressStreet,
                AddressCity = contacts.Contact.AddressCity,
                AddressCountry = contacts.Contact.AddressCountry,
                Phones = contacts.Contact.UserPhone.Select(x => x.Phone).ToList()
            });

            return Ok(contactList);
        }

        // Method: POST/
        [HttpPost]
        public async Task<ActionResult<User>> Post(User user)
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
            if (id != user.Id) return BadRequest();

            var existingUser = await _context.User.Where(u => u.Id == id).FirstOrDefaultAsync();

            try
            {
                if (existingUser != null)
                {
                    _context.Entry(user).State = EntityState.Modified;

                    await _context.SaveChangesAsync();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return NoContent();
        }

        // Method: DELETE/:id
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _context.User.Where(u => u.Id == id).FirstOrDefaultAsync();
            var uContacts = await _context.UserContact.Where(u => u.UserId == id || u.ContactId == id).ToListAsync();
            var uPhones = await _context.UserPhone.Where(u => u.UserId == id).ToListAsync();
            var uSms = await _context.UserSm.Where(u => u.UserId == id).ToListAsync();

            if (user == null) return NotFound();

            foreach (var sm in uSms)
            {
                _context.UserSm.Remove(sm);
            }        

            foreach (var phone in uPhones)
            {
                _context.UserPhone.Remove(phone);
            }

            foreach (var contact in uContacts)
            {
                _context.UserContact.Remove(contact);
            }

             _context.User.Remove(user);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest(new { message = "cant delete this row" });
            }

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

        // private bool UserExists(string id) =>
        //     _context.User.Any(u => u.Id == id);
    }
}