using System.Collections.Generic;
using AddressBook.Api.Models;
using AddressBook.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.Api.Controllers
{
    [ApiController]
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
        public ActionResult<Contact> AddContact(ContactDto contactDto)
        {
            if (ModelState.IsValid)
            {
                var contact = _contactService.AddContact(contactDto);
                return CreatedAtAction(nameof(GetContact), new { id = contact.ContactId }, contact);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Contact> DeleteContact(int id)
        {
            var contact = _contactService.DeleteContact(id);
            if (contact != null)
            {
                return contact;
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Contact> GetContact(int id)
        {
            Contact contact = _contactService.GetContact(id);
            if (contact != null)
            {
                return contact;
            }
            return NotFound();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Contact>> ListContacts(string filters, string sorts)
        {
            return _contactService.ListContacts(filters, sorts);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Contact> UpdateContact(int id, ContactDto contactDto)
        {
            if (ModelState.IsValid)
            {
                Contact contact = _contactService.UpdateContact(id, contactDto);
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
