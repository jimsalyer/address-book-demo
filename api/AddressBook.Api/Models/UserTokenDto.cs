namespace AddressBook.Api.Models
{
    public class UserTokenDto
    {
        public int UserId { get; set; }
        public string EmailAddress { get; set; }
        public string Token { get; set; }
    }
}
