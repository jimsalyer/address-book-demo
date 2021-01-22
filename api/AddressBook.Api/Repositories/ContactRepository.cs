using System.Collections.Generic;
using System.Linq;
using AddressBook.Api.DataAccess;
using AddressBook.Api.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;

namespace AddressBook.Api.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly AddressBookDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ISieveProcessor _sieveProcessor;

        public ContactRepository(AddressBookDbContext dbContext, IMapper mapper, ISieveProcessor sieveProcessor)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _sieveProcessor = sieveProcessor;
        }

        public Contact Add(ContactDto contactDto)
        {
            var contact = _mapper.Map<Contact>(contactDto);
            _dbContext.Add(contact);
            _dbContext.SaveChanges();
            return contact;
        }

        public Contact Delete(int id)
        {
            var contact = _dbContext.Contacts.Find(id);
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

        public List<Contact> List(string filters, string sorts, string defaultSorts)
        {
            var sieveModel = new SieveModel
            {
                Filters = filters,
                Sorts = !string.IsNullOrWhiteSpace(sorts) ? sorts : defaultSorts
            };

            var contacts = _dbContext.Contacts.AsNoTracking();
            var contactsResult = _sieveProcessor.Apply(sieveModel, contacts);
            return contactsResult.ToList();
        }

        public Contact Update(int id, ContactDto contactDto)
        {
            var contact = _dbContext.Contacts.Find(id);
            if (contact != null)
            {
                _mapper.Map(contactDto, contact);
                _dbContext.Contacts.Update(contact);
                _dbContext.SaveChanges();
            }
            return contact;
        }
    }
}
