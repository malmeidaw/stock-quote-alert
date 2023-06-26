using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace stock_quote_alert
{
    internal interface IAPIRequest
    {
        Task<double> LastPrice(string Symbol);
    }
    public class StockData
    {
        public string symbol { get; set; }
        public double lastPrice { get; set; }
    }
    public class APIRequest : IAPIRequest
    {
        public async Task<double> LastPrice(string Symbol)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://mfinance.com.br/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                HttpResponseMessage responseJSON = await client.GetAsync($"api/v1/stocks/{Symbol}");
                responseJSON.EnsureSuccessStatusCode();

                //Showing the status of the http request
                //Console.WriteLine(responseJSON.StatusCode.ToString());

                HttpContent content = responseJSON.Content;
                string data = await content.ReadAsStringAsync();

                //Turning JSON in an object.
                StockData stockData = JsonConvert.DeserializeObject<StockData>(data);

                client.Dispose();
                return stockData.lastPrice;

            }
            catch (HttpRequestException ex) 
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
            
        }
    }
}
