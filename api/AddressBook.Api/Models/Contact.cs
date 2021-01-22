using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AddressBook.Api.Models
{
    public class Contact : ContactDto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContactId { get; set; }

        public ICollection<Tag> Tags { get; set; }
    }
}
