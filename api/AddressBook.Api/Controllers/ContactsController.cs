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
        public ActionResult<Contact> Add(ContactDto contactDto)
        {
            if (ModelState.IsValid)
            {
                var contact = _contactService.Add(contactDto);
                return CreatedAtAction(nameof(Get), new { id = contact.ContactId }, contact);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Contact> Delete(int id)
        {
            var contact = _contactService.Delete(id);
            if (contact != null)
            {
                return contact;
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Contact> Get(int id)
        {
            Contact contact = _contactService.Get(id);
            if (contact != null)
            {
                return contact;
            }
            return NotFound();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Contact>> List()
        {
            return _contactService.List();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Contact> Update(int id, ContactDto contactDto)
        {
            if (ModelState.IsValid)
            {
                Contact contact = _contactService.Update(id, contactDto);
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
