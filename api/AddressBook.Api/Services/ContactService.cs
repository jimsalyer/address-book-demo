using System.Collections.Generic;
using System.Threading.Tasks;
using AddressBook.Api.Models;
using AddressBook.Api.Repositories;

namespace AddressBook.Api.Services
{
    public interface IContactService
    {
        Task<Contact> AddContactAsync(ContactDto contactDto);
        Task<Contact> DeleteContactAsync(int contactId);
        Task<Contact> GetContactAsync(int contactId);
        Task<List<Contact>> ListContactsAsync(string filters, string sorts);
        Task<Contact> UpdateContactAsync(int contactId, ContactDto contactDto);
    }

    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<Contact> AddContactAsync(ContactDto contactDto)
        {
            return await _contactRepository.AddContactAsync(contactDto);
        }

        public async Task<Contact> DeleteContactAsync(int contactId)
        {
            return await _contactRepository.DeleteContactAsync(contactId);
        }

        public async Task<Contact> GetContactAsync(int contactId)
        {
            return await _contactRepository.GetContactAsync(contactId);
        }

        public async Task<List<Contact>> ListContactsAsync(string filters, string sorts)
        {
            return await _contactRepository.ListContactsAsync(filters, sorts, "lastName,firstName,middleName,displayName");
        }

        public async Task<Contact> UpdateContactAsync(int contactId, ContactDto contactDto)
        {
            return await _contactRepository.UpdateContactAsync(contactId, contactDto);
        }
    }
}
