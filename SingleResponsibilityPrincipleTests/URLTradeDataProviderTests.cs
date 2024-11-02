using Microsoft.Data.SqlClient;
using SingleResponsibilityPrinciple.Contracts;
using SingleResponsibilityPrinciple;

namespace SingleResponsibilityPrinciple.Tests
{
    [TestClass()]
    public class URLTradeDataProviderTests
    {
        private int countStrings(IEnumerable<string> collectionOfStrings)
        {
            // count the trades
            int count = 0;
            foreach (var trade in collectionOfStrings)
            {
                count++;
            }
            return count;
        }


        [TestMethod()]
        public void TestSize1()
        {
            //Arrange
            ILogger logger = new ConsoleLogger();
            string tradeURL = "http://raw.githubusercontent.com/tgibbons-css/CIS3285_Unit9_F24/refs/heads/master/SingleResponsibilityPrinciple/trades.txt";

            ITradeDataProvider tradeProvider = new URLTradeDataProvider(tradeURL, logger);

            //Act
            IEnumerable<string> trades = tradeProvider.GetTradeData();

            //Assert

            Assert.AreEqual(countStrings(trades), 4);
        }
    }
}