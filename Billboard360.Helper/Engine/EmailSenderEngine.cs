using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Net;
using Newtonsoft.Json;


namespace Billboard360.Helper.Engine
{
    public class EmailSenderEngine
    {
        public void SendEmail(MailMessage mailMessage, string setting)
        {
            var config = JsonConvert.DeserializeObject<EmailConfig>(setting);

            SmtpClient smptClient = new SmtpClient(config.SMTP);
            smptClient.Port = config.Port;
            smptClient.Credentials = new NetworkCredential(config.UserName, config.Password);
            smptClient.EnableSsl = config.EnableSSL;

            smptClient.Send(mailMessage);

        }
    }

    class EmailConfig
    {
        public string SMTP { get; set; }
        public string FromAddress { get; set; }
        public int Port { get; set; }
        public bool EnableSSL { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
