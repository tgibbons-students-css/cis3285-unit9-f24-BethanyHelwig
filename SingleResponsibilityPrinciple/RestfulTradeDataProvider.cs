using SingleResponsibilityPrinciple.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple
{
    public class RestfulTradeDataProvider : ITradeDataProvider
    {
        string url;
        ILogger logger;
        HttpClient client = new HttpClient();

        public RestfulTradeDataProvider(string url, ILogger logger)
        {
            this.url = url;
            this.logger = logger;
        }

        async Task<List<string>> GetTradeAsync()
        {
            logger.LogInfo("Connecting to the Restful API using HTTP");
            List<string> tradesString = null;

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                tradesString = JsonSerializer.Deserialize<List<string>>(content);
                logger.LogInfo("Received " + tradesString.Count + " trade strings.");
            }

            return tradesString;
        }

        public IEnumerable<string> GetTradeData()
        {
            Task<List<string>> task = Task.Run(() => GetTradeAsync());
            task.Wait();

            List<string> tradeList = task.Result;
            return tradeList;
        }
    }
}
