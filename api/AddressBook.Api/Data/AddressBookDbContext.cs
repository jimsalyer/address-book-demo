using AddressBook.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.Api.Data
{
    public class AddressBookDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }

        public AddressBookDbContext(DbContextOptions<AddressBookDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSnakeCaseNamingConvention();
    }
}
