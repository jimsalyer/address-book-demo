using System.Collections.Generic;
using System.Threading.Tasks;
using AddressBook.Api.Models;

namespace AddressBook.Api.Repositories
{
    public interface IContactRepository
    {
        Task<Contact> AddContactAsync(ContactDto contactDto);
        Task<Contact> DeleteContactAsync(int id);
        Task<Contact> GetContactAsync(int id);
        Task<List<Contact>> ListContactsAsync(string filters, string sorts, string defaultSorts);
        Task<Contact> UpdateContactAsync(int id, ContactDto contactDto);
    }
}
