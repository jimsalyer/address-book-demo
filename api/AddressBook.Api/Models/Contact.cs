using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;

namespace AddressBook.Api.Models
{
    [AutoMap(typeof(ContactDto))]
    public class Contact : ContactDto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContactId { get; set; }
    }
}
