using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RulesExercise.Infrastructure.Senders.Smtp
{
    public class SmtpConfiguration
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public string SenderUserName { get; set; }

        public string SenderPassword { get; set; }

        public bool EnableSsl { get; set; }

        public string Receiver { get; set; }
    }
}
