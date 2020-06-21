using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billboard360.API.Core
{
    public class AppSettings
    {
        public string URLForgotPassword { get; set; }
        public string BaseURL { get; set; }
        public EmailConfig EmailConfig { get; set; }
        public MidTransConfig MidtransConfig { get; set; }
        public string PDFPath { get; set; }
        public string TemplatePath { get; set; }
        public string PPNDefault { get; set; }
        public string URLSyncProvinceCity { get; set; }
    }

    public class MidTransConfig
    {
        public string MerchantID { get; set;}
        public string ClientKey { get; set; }
        public string ServerKey { get; set; }
        public string URL { get; set;}
    }

    public class EmailConfig
    {
        public string SMTP { get; set; }
        public string FromAddress { get; set; }
        public int Port { get; set; }
        public bool EnableSSL { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

    }


}
