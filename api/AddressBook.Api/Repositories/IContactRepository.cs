using System.Collections.Generic;
using AddressBook.Api.Models;

namespace AddressBook.Api.Repositories
{
    public interface IContactRepository
    {
        Contact Add(ContactDto contactDto);
        Contact Delete(int id);
        Contact Get(int id);
        List<Contact> List();
        Contact Update(int id, ContactDto contactDto);
    }
}
