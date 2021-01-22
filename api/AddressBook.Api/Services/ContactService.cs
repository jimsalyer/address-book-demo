using System.Collections.Generic;
using AddressBook.Api.Models;
using AddressBook.Api.Repositories;

namespace AddressBook.Api.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public Contact AddContact(ContactDto contactDto)
        {
            return _contactRepository.AddContact(contactDto);
        }

        public Contact DeleteContact(int id)
        {
            return _contactRepository.DeleteContact(id);
        }

        public Contact GetContact(int id)
        {
            return _contactRepository.GetContact(id);
        }

        public List<Contact> ListContacts(string filters, string sorts)
        {
            return _contactRepository.ListContacts(filters, sorts, "lastName,firstName,middleName,displayName");
        }

        public Contact UpdateContact(int id, ContactDto contactDto)
        {
            return _contactRepository.UpdateContact(id, contactDto);
        }
    }
}
