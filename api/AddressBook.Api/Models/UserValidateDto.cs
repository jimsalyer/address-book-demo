using System.ComponentModel.DataAnnotations;

namespace AddressBook.Api.Models
{
    public class UserValidateDto : UserProfileDto
    {
        [DataType(DataType.Password)]
        [MaxLength(255)]
        [Required]
        public string Password { get; set; }
    }
}
