using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ubs.Equities.Core.Calculators;

namespace Ubs.Equities.Core.MsTest.Calculators
{
    [TestClass]
    public class TestCalculator
    {
        #region Public methods

        [TestMethod]
        public void CalculateMarketValueShouldReturnMultiplyingPriceAndQuantity()
        {
            const decimal Price = 10.12m;
            const long Quantity = 5;

            var mock = new Mock<StockCalculator>();

            decimal result = mock.Object.CalculateMarketValue(Price, Quantity);

            Assert.AreEqual(Price * Quantity, result);
        }

        [TestMethod]
        public void CalculateStockWeightShouldReturnMarketValuePercentageOfTheTotalMarketValueOfTheFund()
        {
            const decimal MarketValue = 10.12m;
            const long TotalMarketValue = 500;

            var mock = new Mock<StockCalculator>();

            decimal result = mock.Object.CalculateStockWeight(MarketValue, TotalMarketValue);

            Assert.AreEqual(MarketValue * 100 / TotalMarketValue, result);
        }

        #endregion
    }
}