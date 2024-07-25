using EmailAPI.Models.Entities;

namespace EmailAPI.Models.DTOs
{
    public class CreateEmailDto
    {
        public required string Sender { get; set; }
        public required string Recipient { get; set; }
        public required string Subject { get; set; }
        public required string Body { get; set; }
    }
}
