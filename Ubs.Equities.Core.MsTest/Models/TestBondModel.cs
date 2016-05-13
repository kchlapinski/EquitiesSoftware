using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ubs.Equities.Core.Models;

namespace Ubs.Equities.Core.MsTest.Models
{
    [TestClass]
    public class TestBondModel
    {
        #region Public methods

        [TestMethod]
        public void BondModelShouldBeInvalidWhenTransactionCostIsGreaterThanTolerance()
        {
            var bondModel = new BondModel
            {
                Name = "Bond1",
                Price = 10000001m,
                Quantity = 1
            };

            Assert.IsFalse(bondModel.IsValid);
        }

        [TestMethod]
        public void BondModelShouldBeValidWhenTransactionCostIsLessThanTolerance()
        {
            var bondModel = new BondModel
            {
                Name = "Bond1",
                Price = 1001m,
                Quantity = 1
            };

            Assert.IsTrue(bondModel.IsValid);
        }

        #endregion
    }
}