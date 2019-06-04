using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HSourcer.Infrastructure.Options
{
    public class MailConfig
    {
        public string Key { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
    }
}
