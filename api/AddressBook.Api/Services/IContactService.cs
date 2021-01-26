using System.Collections.Generic;
using System.Threading.Tasks;
using AddressBook.Api.Models;

namespace AddressBook.Api.Services
{
    public interface IContactService
    {
        Task<Contact> AddContactAsync(ContactDto contactDto);
        Task<Contact> DeleteContactAsync(int id);
        Task<Contact> GetContactAsync(int id);
        Task<List<Contact>> ListContactsAsync(string filters, string sorts);
        Task<Contact> UpdateContactAsync(int id, ContactDto contactDto);
    }
}
