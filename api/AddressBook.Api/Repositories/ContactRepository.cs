using System.Collections.Generic;
using System.Linq;
using AddressBook.Api.DataAccess;
using AddressBook.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.Api.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly AddressBookDbContext _dbContext;

        public ContactRepository(AddressBookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Contact Add(ContactDto contactDto)
        {
            Contact contact = (Contact)contactDto.Clone();
            _dbContext.Add(contact);
            _dbContext.SaveChanges();
            return contact;
        }

        public Contact Delete(int id)
        {
            Contact contact = _dbContext.Contacts.Find(id);
            if (contact != null)
            {
                _dbContext.Remove(contact);
                _dbContext.SaveChanges();
            }
            return contact;
        }

        public Contact Get(int id)
        {
            return _dbContext.Contacts
                .AsNoTracking()
                .FirstOrDefault(c => c.ContactId == id);
        }

        public List<Contact> List()
        {
            return _dbContext.Contacts
                .AsNoTracking()
                .ToList();
        }

        public Contact Update(int id, ContactDto contactDto)
        {
            Contact contact = _dbContext.Contacts.Find(id);
            if (contact != null)
            {
                contact.FirstName = contactDto.FirstName;
                contact.MiddleName = contactDto.MiddleName;
                contact.LastName = contactDto.LastName;
                contact.DisplayName = contactDto.DisplayName;
                contact.StreetAddress = contactDto.StreetAddress;
                contact.City = contactDto.City;
                contact.Region = contactDto.Region;
                contact.PostalCode = contactDto.PostalCode;
                contact.Country = contactDto.Country;
                contact.PhoneNumber = contactDto.PhoneNumber;
                contact.EmailAddress = contactDto.EmailAddress;

                _dbContext.Contacts.Update(contact);
                _dbContext.SaveChanges();
            }
            return contact;
        }
    }
}
