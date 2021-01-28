using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AddressBook.Api.Models;
using AddressBook.Api.Services;

namespace AddressBook.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/contacts")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Contact>> AddContactAsync(ContactDto contactDto)
        {
            if (ModelState.IsValid)
            {
                var contact = await _contactService.AddContactAsync(contactDto);
                return CreatedAtAction("GetContact", new { id = contact.ContactId }, contact);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Contact>> DeleteContactAsync(int id)
        {
            var contact = await _contactService.DeleteContactAsync(id);
            if (contact != null)
            {
                return contact;
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Contact>> GetContactAsync(int id)
        {
            Contact contact = await _contactService.GetContactAsync(id);
            if (contact != null)
            {
                return contact;
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> ListContactsAsync(string filters, string sorts)
        {
            return await _contactService.ListContactsAsync(filters, sorts);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Contact>> UpdateContactAsync(int id, ContactDto contactDto)
        {
            if (ModelState.IsValid)
            {
                Contact contact = await _contactService.UpdateContactAsync(id, contactDto);
                if (contact != null)
                {
                    return contact;
                }
                return NotFound();
            }
            return BadRequest();
        }
    }
}
