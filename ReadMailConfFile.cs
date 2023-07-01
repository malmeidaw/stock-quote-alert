using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace StockQuoteAlert
{
    internal interface IReadMailConfFile
    {
        public MailConf MailConfFromFile();

    }
    public class MailConfRead : IReadMailConfFile
    {
        public MailConf MailConfFromFile()
        {
            {
                const string path = "MailConfFile.json";
                var serializer = new JsonSerializer();
                MailConf? conf = new MailConf();

                try
                {
                    using (var streamReader = new StreamReader(path))
                    using (var jsonText = new JsonTextReader(streamReader))
                    {
                        conf = serializer.Deserialize<MailConf>(jsonText);
                    }
                    if (conf == null) throw new ArgumentNullException();
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine("\nError opening file.\n");
                    Console.WriteLine(e.Message);   
                }
                return conf;

            }
        }
    }
    public class MailConf
    {
        public string? toAddress { get; set; }
        public string? fromAddress { get; set; }
        public string? smtpHost { get; set; }
        public int smtpPort { get; set; }
        public string? smtpUsername { get; set; }
        public string? smtpPassword { get; set; }


    }
}
