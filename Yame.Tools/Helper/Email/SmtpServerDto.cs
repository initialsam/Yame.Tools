using System;
using System.Collections.Generic;
using System.Text;

namespace Yame.Tools.Helper
{
    public class SmtpServerDto
    {
        public long Id { get; set; }
        public string SMTP { get; set; }
        public int SMTPport { get; set; }
        public string SenderMailAddress { get; set; }
        public string SenderAccount { get; set; }
        public string SenderPassword { get; set; }
        public bool SslRequired { get; set; }
        public bool AuthRequired { get; set; }
    }
}
