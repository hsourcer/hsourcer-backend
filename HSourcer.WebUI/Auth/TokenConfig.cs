using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HSourcer.WebUI.Auth
{
    public class TokenConfig
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
    }
}
