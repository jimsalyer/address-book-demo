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

        public Contact Add(ContactDto contactDto)
        {
            return _contactRepository.Add(contactDto);
        }

        public Contact Delete(int id)
        {
            return _contactRepository.Delete(id);
        }

        public Contact Get(int id)
        {
            return _contactRepository.Get(id);
        }

        public List<Contact> List(string filters, string sorts)
        {
            return _contactRepository.List(filters, sorts, "lastName,firstName,middleName,displayName");
        }

        public Contact Update(int id, ContactDto contactDto)
        {
            return _contactRepository.Update(id, contactDto);
        }
    }
}
