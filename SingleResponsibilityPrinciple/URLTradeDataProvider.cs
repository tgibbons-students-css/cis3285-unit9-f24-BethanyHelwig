using SingleResponsibilityPrinciple.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple
{
    public class URLTradeDataProvider : ITradeDataProvider
    {
        string url;
        ILogger logger;

        public URLTradeDataProvider(string url, ILogger logger)
        {
            this.url = url;
            this.logger = logger;
        }

        public IEnumerable<string> GetTradeData()
        {
            List<string> tradeData = new List<string>();
            logger.LogInfo("Reading trades from URL: " + url);

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(url).Result;
                if (!response.IsSuccessStatusCode)
                {
                    // log error and throw an exception if the URL fails
                    logger.LogWarning($"Failed to retrieve data. Status code: {response.StatusCode}");
                    throw new Exception($"Error retrieving data from URL: {url}");
                }
                // set up a Stream and StreamReader to access the data
                using (Stream stream = response.Content.ReadAsStreamAsync().Result)
                using (StreamReader reader = new StreamReader(stream))
                {
                    // Read each line of the text file using reader.ReadLine()
                    // Read until the reader returns a null line
                    // Add each line to the tradeData list
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        tradeData.Add(line);
                    }
                }
            }

            return tradeData;
        }
    }
}
