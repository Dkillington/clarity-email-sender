namespace EmailSenderWPF.Scripts
{
    // The format for an email sent from WPF Frontend to API Backend
    internal class SendableEmail
    {
        public required string Sender { get; set; }
        public required string Recipient { get; set; }
        public required string Subject { get; set; }
        public required string Body { get; set; }
    }
}
