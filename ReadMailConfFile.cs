using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace stock_quote_alert
{
    internal interface ReadMailConfFile
    {
        public MailConf MailConfFromFile();

    }
    public class MailConfRead : ReadMailConfFile
    {
        public MailConf MailConfFromFile()
        {
            {
                const string path = "MailConfFile.json";
                var serializer = new JsonSerializer();
                MailConf conf = new MailConf();
                using (var streamReader = new StreamReader(path))
                using (var jsonText = new JsonTextReader(streamReader))
                {
                    conf = serializer.Deserialize<MailConf>(jsonText);
                }
                
                
                

                return conf;
            }
        }
    }
    public class MailConf
    {
        public string ToAddress { get; set; }
        public string FromAddress { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
    }
}
