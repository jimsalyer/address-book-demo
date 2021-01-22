using System.ComponentModel.DataAnnotations;

namespace AddressBook.Api.Models
{
    public class ContactDto
    {
        [MaxLength(255)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(255)]
        public string MiddleName { get; set; }

        [MaxLength(255)]
        [Required]
        public string LastName { get; set; }

        [MaxLength(255)]
        [Required]
        public string DisplayName { get; set; }

        [Required]
        public string StreetAddress { get; set; }

        [MaxLength(255)]
        [Required]
        public string City { get; set; }

        [MaxLength(255)]
        [Required]
        public string Region { get; set; }

        [MaxLength(255)]
        [Required]
        public string PostalCode { get; set; }

        [MaxLength(255)]
        [Required]
        public string Country { get; set; }

        [MaxLength(255)]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        [MaxLength(255)]
        public string EmailAddress { get; set; }

        public ContactDto Clone()
        {
            return (ContactDto)MemberwiseClone();
        }
    }
}
