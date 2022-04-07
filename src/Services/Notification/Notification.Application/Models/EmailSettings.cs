using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Application.Models
{
    public class EmailSettings
    {
        public string FromAddress { get; set; }
        public string FromName { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string SSL { get; set; }
        public NetworkCredentials NetworkCredentials { get; set; }
    }

    public class NetworkCredentials
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
