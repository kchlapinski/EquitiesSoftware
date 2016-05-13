using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ubs.Equities.Core.Models;

namespace Ubs.Equities.Core.MsTest.Models
{
    [TestClass]
    public class TestEquityModel
    {
        [TestMethod]
        public void EquityModelShouldBeInvalidWhenTransactionCostIsGreaterThanTolerance()
        {
            var equityModel = new EquityModel
            {
                Price = 10000001m,
                Quantity = 1
            };

            Assert.IsFalse(equityModel.IsValid);
        }

        [TestMethod]
        public void EquityModelShouldBeValidWhenTransactionCostIsLessThanTolerance()
        {
            var equityModel = new EquityModel
            {
                Price = 1001m,
                Quantity = 1
            };

            Assert.IsTrue(equityModel.IsValid);
        }
    }
}