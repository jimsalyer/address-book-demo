using System.Collections.Generic;
using AddressBook.Api.Models;

namespace AddressBook.Api.Repositories
{
    public interface IContactRepository
    {
        Contact AddContact(ContactDto contactDto);
        Contact DeleteContact(int id);
        Contact GetContact(int id);
        List<Contact> ListContacts(string filters, string sorts, string defaultSorts);
        Contact UpdateContact(int id, ContactDto contactDto);
    }
}
