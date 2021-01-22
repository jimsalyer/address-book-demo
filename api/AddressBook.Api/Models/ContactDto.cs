using System.ComponentModel.DataAnnotations;
using Sieve.Attributes;

namespace AddressBook.Api.Models
{
    public class ContactDto
    {
        [MaxLength(255)]
        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public string FirstName { get; set; }

        [MaxLength(255)]
        [Sieve(CanFilter = true, CanSort = true)]
        public string MiddleName { get; set; }

        [MaxLength(255)]
        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public string LastName { get; set; }

        [MaxLength(255)]
        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public string DisplayName { get; set; }

        [Required]
        [Sieve(CanFilter = true)]
        public string StreetAddress { get; set; }

        [MaxLength(255)]
        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public string City { get; set; }

        [MaxLength(255)]
        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public string Region { get; set; }

        [MaxLength(255)]
        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public string PostalCode { get; set; }

        [MaxLength(255)]
        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public string Country { get; set; }

        [MaxLength(255)]
        [Sieve(CanFilter = true, CanSort = true)]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        [MaxLength(255)]
        [Sieve(CanFilter = true, CanSort = true)]
        public string EmailAddress { get; set; }

        public ContactDto Clone()
        {
            return (ContactDto)MemberwiseClone();
        }
    }
}
