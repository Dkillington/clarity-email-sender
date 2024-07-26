using EmailAPI.Models.Entities;

namespace EmailAPI.Models.DTOs
{
    public class UpdateEmailDto
    {
        public required string Sender { get; set; }
        public required string Recipient { get; set; }
        public required string Subject { get; set; }
        public required string Body { get; set; }
        public required EmailStatus Status { get; set; }
    }
}
