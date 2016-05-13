using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ubs.Equities.Core.Calculators;

namespace Ubs.Equities.Core.MsTest.Calculators
{
    [TestClass]
    public class TestEquityCalculator
    {
        #region Public methods

        [TestMethod]
        public void EquityCalculateTransactionCostShouldReturnMultiplyingMarketValueAnd005()
        {
            const decimal MarketValue = 10.12m;

            var calculator = new EquityCalculator();

            decimal result = calculator.CalculateTransactionCost(MarketValue);

            Assert.AreEqual(MarketValue * 0.05m, result);
        }

        #endregion
    }
}