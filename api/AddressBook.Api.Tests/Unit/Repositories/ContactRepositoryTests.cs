using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AddressBook.Api.Data;
using AddressBook.Api.Models;
using AddressBook.Api.Repositories;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;
using Xunit;

namespace AddressBook.Api.Tests.Unit.Repositories
{
    public class ContactRepositoryTests : IDisposable
    {
        private AddressBookDbContext _dbContext;
        private ContactRepository _contactRepository;
        private bool disposed;

        public ContactRepositoryTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<AddressBookDbContext>()
                .UseInMemoryDatabase("AddressBookDatabase")
                .Options;
            _dbContext = new AddressBookDbContext(dbContextOptions);

            _dbContext.Contacts.AddRange(new List<Contact>
            {
                new Contact
                {
                    ContactId = 1,
                    FirstName = "Test First Name 1",
                    MiddleName = "Test Middle Name 1",
                    LastName = "Test Last Name 1",
                    DisplayName = "Test Display Name 1",
                    StreetAddress = "Test Street Address 1",
                    City = "Test City 1",
                    Region = "TO",
                    PostalCode = "12345",
                    Country = "Test Country 1",
                    PhoneNumber = "(012) 345-6789",
                    EmailAddress = "test1@test.com"
                },
                new Contact
                {
                    ContactId = 2,
                    FirstName = "Test First Name 2",
                    MiddleName = "Test Middle Name 2",
                    LastName = "Test Last Name 2",
                    DisplayName = "Test Display Name 2",
                    StreetAddress = "Test Street Address 2",
                    City = "Test City 2",
                    Region = "TT",
                    PostalCode = "67890",
                    Country = "Test Country 2",
                    PhoneNumber = "(123) 456-7890",
                    EmailAddress = "test2@test.com"
                }
            });

            _dbContext.SaveChanges();

            var mapperConfig = new MapperConfiguration(
                config => config.CreateMap<ContactDto, Contact>()
            );
            var mapper = mapperConfig.CreateMapper();

            var sieveProcessorOptions = Options.Create(new SieveOptions());
            var sieveProcessor = new SieveProcessor(sieveProcessorOptions);

            _contactRepository = new ContactRepository(_dbContext, mapper, sieveProcessor);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    var allContacts = _dbContext.Contacts.ToList();
                    _dbContext.Contacts.RemoveRange(allContacts);
                    _dbContext.SaveChanges();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        [Fact]
        public async Task AddContactAsync_WithValidContact_ReturnsAddedContact_WithGeneratedId()
        {

        }

        [Fact]
        public async Task DeleteContactAsync_WithValidContactId_RemovesAndReturnsContact()
        {
            var contactId = 2;
            var deletedContact = await _dbContext.Contacts
                .AsNoTracking()
                .SingleOrDefaultAsync(c => c.ContactId == contactId);

            var actualContact = await _contactRepository.DeleteContactAsync(contactId);
            var expectedContact = await _dbContext.Contacts
                .AsNoTracking()
                .SingleOrDefaultAsync(c => c.ContactId == contactId);

            actualContact.Should().BeEquivalentTo(deletedContact);
            expectedContact.Should().BeNull();
        }

        [Fact]
        public async Task DeleteContactAsync_WithInvalidContactId_ReturnsNull()
        {
            var contactId = 3;

            var actualContact = await _contactRepository.DeleteContactAsync(contactId);

            actualContact.Should().BeNull();
        }

        [Fact]
        public async Task GetContactAsync_WithValidContactId_ReturnsContact()
        {
            var contactId = 1;
            var expectedContact = await _dbContext.Contacts
                .AsNoTracking()
                .SingleOrDefaultAsync(c => c.ContactId == contactId);

            var actualContact = await _contactRepository.GetContactAsync(contactId);

            actualContact.Should().BeEquivalentTo(expectedContact);
        }

        [Fact]
        public async Task GetContactAsync_WithInvalidContactId_ReturnsNull()
        {
            var contactId = 3;

            var actualContact = await _contactRepository.GetContactAsync(contactId);

            actualContact.Should().BeNull();
        }
    }
}
