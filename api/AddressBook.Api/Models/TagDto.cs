using System.ComponentModel.DataAnnotations;

namespace AddressBook.Api.Models
{
    public class TagDto
    {
        [MaxLength(255)]
        [Required]
        public string TagName { get; set; }

        public Tag Clone()
        {
            return (Tag)MemberwiseClone();
        }
    }
}
