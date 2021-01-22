using System.Collections.Generic;
using AddressBook.Api.Models;

namespace AddressBook.Api.Services
{
    public interface IContactService
    {
        Contact AddContact(ContactDto contactDto);
        Contact DeleteContact(int id);
        Contact GetContact(int id);
        List<Contact> ListContacts(string filters, string sorts);
        Contact UpdateContact(int id, ContactDto contactDto);
    }
}
