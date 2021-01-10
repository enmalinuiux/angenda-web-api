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
    public class ContactController : ControllerBase
    {
        private readonly agendaContext _context;

        public ContactController(agendaContext context)
        {
            _context = context;
        }

        // Method: GET/:id/contact/
        [Authorize]
        [HttpGet("{userId}")]
        public async Task<ActionResult<ContactDTO>> GetContacts(string userId)
        {
            var Usercontacts = await _context.UserContact
                .Include(c => c.Contact)
                .Include(p =>p.User.UserPhone)
                .Where(u => u.UserId == userId).ToListAsync();            

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

        // Method: GET/:id/contact/:contactId
        [Authorize]
        [HttpGet("{userId}/{contactId}")]
        public async Task<ActionResult<ContactDTO>> GetContact(string userId, string contactId)
        {
            var contact = await _context.UserContact
                .Include (c => c.Contact)
                .Include (p => p.User.UserPhone)
                .Where   (u => u.UserId == userId && u.ContactId == contactId)
                .FirstOrDefaultAsync();  

            if (contact == null) return NotFound();

            var contactDTO = new ContactDTO{
                Id = contact.ContactId,
                Name = contact.Contact.Name,
                LastName = contact.Contact.LastName,
                Email = contact.Contact.Email,
                Birth = contact.Contact.Birth,
                UserType = contact.Contact.UserType,
                AddressStreet = contact.Contact.AddressStreet,
                AddressCity = contact.Contact.AddressCity,
                AddressCountry = contact.Contact.AddressCountry,
                Phones = contact.Contact.UserPhone
                    .Select(x => x.Phone)
                    .ToList()
            };

            return Ok(contactDTO);
        }

        // Method: POST/
        [HttpPost]
        public async Task<ActionResult<ContactDTO>> Post(UserContact contact)
        {
            await _context.UserContact.AddAsync(contact);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                return BadRequest(e.InnerException);
            }

            return Ok(contact);
        }

        // Method: PUT/:userId/:contactId
        [Authorize]
        [HttpPut("{userId}/{contactId}")]
        public async Task<IActionResult> Put(string userId, string contactId, UserContact contact)
        {
            if (contactId != contact.ContactId && userId != contact.UserId) return BadRequest();

            var existingContact = await _context.UserContact
                .Where(c => c.UserId == userId && c.ContactId == contactId)
                .FirstOrDefaultAsync();

            try
            {
                if (existingContact != null)
                {
                    _context.Entry(contact).State = EntityState.Modified;

                    await _context.SaveChangesAsync();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                BadRequest(ex.InnerException);
            }

            return NoContent();
        }

        // Method: DELETE/:userId/:contactId
        [Authorize]
        [HttpDelete("{userId}/{contactId}")]
        public async Task<IActionResult> Delete(string userId, string contactId)
        {
            var contact = await _context.UserContact.Where(c => c.UserId == userId && c.ContactId == contactId).FirstOrDefaultAsync();

            if (contact == null) return NotFound();

            _context.UserContact.Remove(contact);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest(new { message = "You can't delete this contact!" });
            }

            return NoContent();
        }
    }
}