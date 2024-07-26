namespace EmailAPI.Models.Entities
{
    // The email stored in the database
    public class Email
    {
        public int Id { get; set; }
        public required string Sender { get; set; }
        public required string Recipient { get; set; }
        public required string Subject { get; set; }
        public required string Body { get; set; }
        public EmailStatus Status { get; set; }
        public DateTime TimeSent { get; set; } = DateTime.UtcNow;

    }

    // Status of send attached to each email
    public enum EmailStatus
    {
        Sent,
        Failed,
    }


}
