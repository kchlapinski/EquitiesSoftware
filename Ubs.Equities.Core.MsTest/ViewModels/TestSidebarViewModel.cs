using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ubs.Equities.Core.Calculators;
using Ubs.Equities.Core.Models;
using Ubs.Equities.Core.Services;
using Ubs.Equities.Core.ViewModels;
using Ubs.Equities.EntityFramework;

namespace Ubs.Equities.Core.MsTest.ViewModels
{
    [TestClass]
    public class TestSidebarViewModel
    {
        #region Public methods

        [TestMethod]
        public void SidebarViewModelInitializationShoudInvokeSubscribeStockAddedEventAndRefreshData()
        {
            var foundServiceMock = new Mock<IFoundService>();
            foundServiceMock.Setup(f => f.GetStocks()).Returns(new List<StockModel>());

            var viewModelMock = new Mock<SidebarViewModel>(foundServiceMock.Object, new EventAggregator())
            {
                CallBase = true
            };
            viewModelMock.Setup(v => v.SubscribeStockAddedEvent());
            viewModelMock.Setup(v => v.RefreshData(It.IsAny<string>()));

            var viewModel = viewModelMock.Object;

            Assert.IsNotNull(viewModel);

            viewModelMock.VerifyAll();
        }

        [TestMethod]
        public void SidebarViewModelInitializationShoudSubscribeStockAddedEvent()
        {
            EventAggregator eventAggregator = new EventAggregator();

            var sidebarViewModel = GetSidebarViewModel(new List<StockModel>(), eventAggregator);

            Assert.IsTrue(eventAggregator.GetEvent<StockAddedEvent>().Contains(sidebarViewModel.RefreshData));
        }

        [TestMethod]
        public void TotalMarketValueShouldBeSumOfBondTotalMarketValueAndEquityTotalMarketValue()
        {
            List<StockModel> stockModels = GetStockModelList();

            SidebarViewModel sidebarViewModel = GetSidebarViewModel(stockModels, new EventAggregator());

            decimal bondTotalMarketValue = stockModels.Where(s => s.Type == StockType.Bond).Sum(s => s.MarketValue);
            Assert.AreEqual(bondTotalMarketValue, sidebarViewModel.BondTotalMarketValue);

            decimal equityTotalMarketValue = stockModels.Where(s => s.Type == StockType.Equity).Sum(s => s.MarketValue);
            Assert.AreEqual(equityTotalMarketValue, sidebarViewModel.EquityTotalMarketValue);

            Assert.AreEqual(bondTotalMarketValue + equityTotalMarketValue, sidebarViewModel.TotalMarketValue);
        }

        [TestMethod]
        public void TotalNumberShouldBeSumOfBondTotalNumberAndEquityTotalNumber()
        {
            List<StockModel> stockModels = GetStockModelList();

            SidebarViewModel sidebarViewModel = GetSidebarViewModel(stockModels, new EventAggregator());

            decimal bondTotalNumber = stockModels.Count(s => s.Type == StockType.Bond);
            Assert.AreEqual(bondTotalNumber, sidebarViewModel.BondTotalNumber);

            decimal equityTotalNumber = stockModels.Count(s => s.Type == StockType.Equity);
            Assert.AreEqual(equityTotalNumber, sidebarViewModel.EquityTotalNumber);

            Assert.AreEqual(bondTotalNumber + equityTotalNumber, sidebarViewModel.TotalNumber);
        }

        [TestMethod]
        public void TotalWeightShouldBeSumOfBondTotalWeightAndEquityTotalWeight()
        {
            List<StockModel> stockModels = GetStockModelList();

            SidebarViewModel sidebarViewModel = GetSidebarViewModel(stockModels, new EventAggregator());

            decimal bondTotalWeight = stockModels.Where(s => s.Type == StockType.Bond).Sum(s => s.StockWeight);
            Assert.AreEqual(bondTotalWeight, sidebarViewModel.BondTotalWeight);

            decimal equityTotalWeight = stockModels.Where(s => s.Type == StockType.Equity).Sum(s => s.StockWeight);
            Assert.AreEqual(equityTotalWeight, sidebarViewModel.EquityTotalWeight);

            Assert.AreEqual(bondTotalWeight + equityTotalWeight, sidebarViewModel.TotalWeight);
        }

        #endregion
        #region Private methods

        private static SidebarViewModel GetSidebarViewModel(List<StockModel> stockModels, EventAggregator eventAggregator)
        {
            var foundServiceMock = new Mock<IFoundService>();
            foundServiceMock.Setup(f => f.GetStocks()).Returns(stockModels);

            var sidebarViewModel = new SidebarViewModel(foundServiceMock.Object, eventAggregator);
            return sidebarViewModel;
        }

        private static List<StockModel> GetStockModelList()
        {
            var bondModel = new BondModel(new BondCalculator())
            {
                Price = 10m,
                Quantity = 2,
                FoundTotalMarketValue = 56m
            };

            var equityModel = new EquityModel(new EquityCalculator())
            {
                Price = 12m,
                Quantity = 3,
                FoundTotalMarketValue = 56m
            };

            var stockModels = new List<StockModel>
            {
                bondModel,
                equityModel
            };
            return stockModels;
        }

        #endregion
    }
}