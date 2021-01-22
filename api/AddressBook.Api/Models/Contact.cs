using AutoMapper;
using System.ComponentModel.DataAnnotations.Schema;

namespace AddressBook.Api.Models
{
    [AutoMap(typeof(ContactDto))]
    public class Contact : ContactDto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContactId { get; set; }
    }
}
