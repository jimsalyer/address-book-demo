using AddressBook.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.Api.DataAccess
{
    public class AddressBookDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public AddressBookDbContext(DbContextOptions<AddressBookDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSnakeCaseNamingConvention();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Contact>()
                .HasMany(contact => contact.Tags)
                .WithMany(tag => tag.Contacts)
                .UsingEntity(entityTypeBuilder => entityTypeBuilder.ToTable("ContactTags"));
        }
    }
}
