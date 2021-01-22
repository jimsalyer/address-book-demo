using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AddressBook.Api.Models
{
    public class Tag : TagDto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TagId { get; set; }

        public ICollection<Contact> Contacts { get; set; }
    }
}
