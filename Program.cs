namespace StockQuoteAlert
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string symbol;
            double sellingReference;
            double buyingReference;
            if (args.Length == 3) 
            {
                symbol = args[0];
                double.TryParse(args[1], out sellingReference);
                double.TryParse(args[2], out buyingReference);
            }
            else
            {
                Console.Write("Symbol SellingReference BuyingReference: ");
                try
                {
                    string? userInput = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(userInput)) throw new ArgumentNullException();
                    char[] Separator = { ' ' };
                    string[] argsInput = userInput.Split(Separator);
                    if (argsInput.Length != 3) throw new ArgumentException();
                    symbol = argsInput[0];
                    double.TryParse(argsInput[1], out sellingReference);
                    double.TryParse(argsInput[2], out buyingReference);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Parameters should be Symbol, SellingReference and BuyingReference\n");
                    Console.WriteLine(ex.Message);
                    return;
                }
            }

            double lastPrice = -1;
            string recommendationBuying = String.Format("Buy {0}", symbol);
            string emailBodyBuying = String.Format("We recommend buying a position in {0}, as its is now lower than {1}. Current price: ", symbol, buyingReference);
            string recommendationSelling = String.Format("Sell {0}", symbol);
            string emailBodySelling = String.Format("We recommend selling your position in {0}, as its price has exceeded the value of {1}. Current price: ", symbol,  sellingReference);

            while (true)
            {
                APIRequest a = new APIRequest();
                double price = a.LastPrice(symbol).Result;
                if (price == -1) break;
                
                if (lastPrice!=price)
                {
                    if (price >= sellingReference)
                    {
                        if (!SendMail(recommendationSelling, emailBodySelling + price)) break;
                    }
                    if (price <= buyingReference)
                    {
                        if (!SendMail(recommendationBuying, emailBodyBuying + price)) break;
                    }
                }
                Console.WriteLine($"Monitoring {symbol}. Current price is {price}");
                lastPrice = price;
                
                
                Thread.Sleep(10000);
                
            }
            
            Console.ReadKey();
            }
        static bool SendMail(string recommendation,string emailBody)
        {
            Mail sellingMail = new Mail();
            MailConfRead mailConfRead = new MailConfRead();
            MailConf mailConf = mailConfRead.MailConfFromFile();
            if (mailConf is null) return false;
            return sellingMail.sendMail(mailConf, recommendation, emailBody);
            
        }
    }
}