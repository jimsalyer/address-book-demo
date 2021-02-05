using Microsoft.EntityFrameworkCore;
using AddressBook.Api.Helpers;
using AddressBook.Api.Models;

namespace AddressBook.Api.Data
{
    public class AddressBookDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<User> Users { get; set; }

        public AddressBookDbContext(DbContextOptions<AddressBookDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            HashUtilities.CreateHash("@dmin123", out byte[] passwordHash, out byte[] passwordSalt);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    EmailAddress = "admin@admin.com",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                }
            );
        }
    }
}
