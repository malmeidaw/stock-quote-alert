namespace stock_quote_alert
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string Symbol;
            double SellingReference;
            double BuyingReference;
            try
            {
                Symbol = args[0];
                SellingReference = double.Parse(args[1]);
                BuyingReference = double.Parse(args[2]);
            }
            catch (Exception) 
            {
                Console.WriteLine();
                string UserInput = Console.ReadLine();
                char[] Separator = { ' ' };
                string[] argsInput = UserInput.Split(Separator);
                Symbol = argsInput[0];
                SellingReference = double.Parse(argsInput[1]);
                BuyingReference = double.Parse(argsInput[2]);
            }
            
            while (true)
            {
                APIRequest a = new APIRequest();
                double price = a.LastPrice(Symbol).Result;
                if (price >= SellingReference)
                {
                    Mail SellingMail = new Mail();
                    MailConfRead mailConfRead = new MailConfRead();
                    MailConf mailConf = new MailConf();
                    mailConf = mailConfRead.MailConfFromFile();

                    string Recommendation = String.Format("Sell {0}", Symbol);
                    string EmailBody = String.Format("We recommend selling your position in {0}, as its current price of {1} has exceeded the value of {2}.", Symbol, price, SellingReference);

                    SellingMail.sendMail(mailConf,Recommendation,EmailBody);
                }
                if (price <= BuyingReference)
                {
                    Mail SellingMail = new Mail();
                    MailConfRead mailConfRead = new MailConfRead();
                    MailConf mailConf = new MailConf();
                    mailConf = mailConfRead.MailConfFromFile();

                    string Recommendation = String.Format("Buy {0}", Symbol);
                    string EmailBody = String.Format("We recommend buying a position in {0}, as its current price of {1} is lower than {2}.", Symbol, price, BuyingReference);

                    SellingMail.sendMail(mailConf, Recommendation, EmailBody);
                }
                Console.WriteLine(price);
                //Thread.Sleep(60000);
                Thread.Sleep(6000);
            }
            //double price = a.LastPrice(Symbol).Result;
            
            //Console.WriteLine(Symbol);
            //Console.WriteLine(SellingReference);
            //Console.WriteLine(BuyingReference);
            //Console.ReadKey();
            }
    }
}