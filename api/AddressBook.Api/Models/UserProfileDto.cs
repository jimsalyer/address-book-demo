using System.ComponentModel.DataAnnotations;

namespace AddressBook.Api.Models
{
    public class UserProfileDto
    {
        [MaxLength(255)]
        [EmailAddress]
        [Required]
        public string EmailAddress { get; set; }
    }
}
