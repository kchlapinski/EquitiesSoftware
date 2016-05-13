using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using Ubs.Equities.Core.Calculators;
using Ubs.Equities.Core.Models;

namespace Ubs.Equities.Core.MsTest.Models
{
    [TestClass]
    public class TestStockModel
    {
        [TestMethod]
        public void BondModelShouldBeInvalidWhenMarketValueIsLessThanZero()
        {
            var stockModelMock = new Mock<StockModel>
            {
                CallBase = true
            };
            stockModelMock.Protected().SetupGet<IStockCalculator>("StockCalculator").Returns(new BondCalculator());
            
            stockModelMock.Object.Price = -1m;
            stockModelMock.Object.Quantity = 2;

            Assert.IsFalse(stockModelMock.Object.IsValid);
        }

        [TestMethod]
        public void BondModelShouldBeValidWhenMarketValueIsGreaterThanZero()
        {
            var stockModelMock = new Mock<StockModel>
            {
                CallBase = true
            };
            stockModelMock.Protected().SetupGet<IStockCalculator>("StockCalculator").Returns(new BondCalculator());
            stockModelMock.Protected().SetupGet<decimal>("Tolerance").Returns(10000);

            stockModelMock.Object.Price = 5m;
            stockModelMock.Object.Quantity = 2;

            Assert.IsTrue(stockModelMock.Object.IsValid);
        }
    }
}