using System.ComponentModel.DataAnnotations;

namespace AddressBook.Api.Models
{
    public class UserPasswordDto
    {
        [DataType(DataType.Password)]
        [MaxLength(255)]
        [MinLength(8)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]+$")]
        [Required]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
