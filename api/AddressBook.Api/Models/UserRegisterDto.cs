using System.ComponentModel.DataAnnotations;

namespace AddressBook.Api.Models
{
    public class UserRegisterDto : UserPasswordDto
    {
        [MaxLength(255)]
        [EmailAddress]
        [Required]
        public string EmailAddress { get; set; }
    }
}
