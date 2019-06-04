using System.Collections.Generic;

namespace HSourcer.Application.Notifications.Models
{
    public class Message
    {
        public List<string> To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
    public class EmailFrom
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
