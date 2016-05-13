using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ubs.Equities.Core.Calculators;

namespace Ubs.Equities.Core.MsTest.Calculators
{
    [TestClass]
    public class TestBondCalculator
    {
        #region Public methods

        [TestMethod]
        public void BondCalculateTransactionCostShouldReturnMultiplyingMarketValueAnd002()
        {
            const decimal MarketValue = 10.12m;

            var calculator = new BondCalculator();

            decimal result = calculator.CalculateTransactionCost(MarketValue);

            Assert.AreEqual(MarketValue * 0.02m, result);
        }

        #endregion
    }
}