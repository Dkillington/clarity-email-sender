using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSenderWPF
{
    internal class SendableEmail
    {
        public required string Sender { get; set; }
        public required string Recipient { get; set; }
        public required string Subject { get; set; }
        public required string Body { get; set; }
    }
}
