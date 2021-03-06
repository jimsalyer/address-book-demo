using System.Collections.Generic;
using System.Threading.Tasks;
using AddressBook.Api.Data;
using AddressBook.Api.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;

namespace AddressBook.Api.Repositories
{
    public interface IContactRepository
    {
        Task<Contact> AddContactAsync(ContactDto contactDto);
        Task<Contact> DeleteContactAsync(int contactId);
        Task<Contact> GetContactAsync(int contactId);
        Task<List<Contact>> ListContactsAsync(string filters, string sorts, string defaultSorts);
        Task<Contact> UpdateContactAsync(int contactId, ContactDto contactDto);
    }

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

        public async Task<Contact> AddContactAsync(ContactDto contactDto)
        {
            var contact = _mapper.Map<Contact>(contactDto);
            await _dbContext.AddAsync(contact);
            await _dbContext.SaveChangesAsync();
            return contact;
        }

        public async Task<Contact> DeleteContactAsync(int contactId)
        {
            var contact = await _dbContext.Contacts.FindAsync(contactId);
            if (contact != null)
            {
                _dbContext.Remove(contact);
                await _dbContext.SaveChangesAsync();
            }
            return contact;
        }

        public async Task<Contact> GetContactAsync(int contactId)
        {
            return await _dbContext.Contacts
                .AsNoTracking()
                .SingleOrDefaultAsync(c => c.ContactId == contactId);
        }

        public async Task<List<Contact>> ListContactsAsync(string filters, string sorts, string defaultSorts)
        {
            var sieveModel = new SieveModel
            {
                Filters = filters,
                Sorts = !string.IsNullOrWhiteSpace(sorts) ? sorts : defaultSorts
            };

            var contacts = _dbContext.Contacts.AsNoTracking();
            var contactsResult = _sieveProcessor.Apply(sieveModel, contacts);
            return await contactsResult.ToListAsync();
        }

        public async Task<Contact> UpdateContactAsync(int contactId, ContactDto contactDto)
        {
            var contact = await _dbContext.Contacts.FindAsync(contactId);
            if (contact != null)
            {
                _mapper.Map(contactDto, contact);
                _dbContext.Contacts.Update(contact);
                await _dbContext.SaveChangesAsync();
            }
            return contact;
        }
    }
}
