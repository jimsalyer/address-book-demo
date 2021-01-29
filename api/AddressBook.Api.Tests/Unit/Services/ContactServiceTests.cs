using System.Threading.Tasks;
using AddressBook.Api.Models;
using AddressBook.Api.Repositories;
using AddressBook.Api.Services;
using Moq;
using Xunit;

namespace AddressBook.Api.Tests.Unit.Services
{
    public class ContactServiceTests
    {
        private readonly Mock<IContactRepository> _contactRepositoryMock;
        private readonly IContactService _contactService;

        public ContactServiceTests()
        {
            _contactRepositoryMock = new Mock<IContactRepository>();
            _contactService = new ContactService(_contactRepositoryMock.Object);
        }

        [Fact]
        public async Task AddContactAsync_CallsRepoAddContactAsync_WithContactDto()
        {
            var contactDto = new ContactDto
            {
                FirstName = "Test First Name",
                MiddleName = "Test Middle Name",
                LastName = "Test Last Name",
                DisplayName = "Test Display Name",
                StreetAddress = "Test Street Address",
                City = "Test City",
                Region = "TR",
                PostalCode = "12345",
                Country = "Test Country",
                PhoneNumber = "(012) 345-6789",
                EmailAddress = "test@test.com"
            };

            await _contactService.AddContactAsync(contactDto);
            _contactRepositoryMock.Verify(repo => repo.AddContactAsync(contactDto), Times.Once);
        }

        [Fact]
        public async Task DeleteContactAsync_CallsRepoDeleteContactAsync_WithContactId()
        {
            var contactId = 1;
            await _contactService.GetContactAsync(contactId);
            _contactRepositoryMock.Verify(repo => repo.GetContactAsync(contactId), Times.Once);
        }

        [Fact]
        public async Task GetContactAsync_CallsRepoGetContactAsync_WithContactId()
        {
            var contactId = 1;
            await _contactService.DeleteContactAsync(contactId);
            _contactRepositoryMock.Verify(repo => repo.DeleteContactAsync(contactId), Times.Once);
        }

        [Fact]
        public async Task ListContactsAsync_CallsRepoContactsAsync_WithFiltersAndSortsAndDefaultSorts()
        {
            var filters = "lastName==Smith";
            var sorts = "displayName";

            await _contactService.ListContactsAsync(filters, sorts);
            _contactRepositoryMock.Verify(repo => repo.ListContactsAsync(filters, sorts, "lastName,firstName,middleName,displayName"), Times.Once);
        }

        [Fact]
        public async Task UpdateContactAsync_CallsRepoUpdateContactAsync_WithContactIdAndContactDto()
        {
            var contactId = 1;
            var contactDto = new ContactDto
            {
                FirstName = "Test First Name",
                MiddleName = "Test Middle Name",
                LastName = "Test Last Name",
                DisplayName = "Test Display Name",
                StreetAddress = "Test Street Address",
                City = "Test City",
                Region = "TR",
                PostalCode = "12345",
                Country = "Test Country",
                PhoneNumber = "(012) 345-6789",
                EmailAddress = "test@test.com"
            };

            await _contactService.UpdateContactAsync(contactId, contactDto);
            _contactRepositoryMock.Verify(repo => repo.UpdateContactAsync(contactId, contactDto), Times.Once);
        }
    }
}
