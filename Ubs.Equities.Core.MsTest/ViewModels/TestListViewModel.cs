using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ubs.Equities.Core.Models;
using Ubs.Equities.Core.Services;
using Ubs.Equities.Core.ViewModels;

namespace Ubs.Equities.Core.MsTest.ViewModels
{
    [TestClass]
    public class TestListViewModel
    {
        #region Public methods

        [TestMethod]
        public void ListViewModelInitializationShoudInvokeSubscribeStockAddedEventAndRefreshData()
        {
            var foundServiceMock = new Mock<IFoundService>();
            foundServiceMock.Setup(f => f.GetStocks()).Returns(new List<StockModel>());

            var viewModelMock = new Mock<ListViewModel>(foundServiceMock.Object, new EventAggregator())
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
        public void ListViewModelInitializationShoudSubscribeStockAddedEvent()
        {
            var foundServiceMock = new Mock<IFoundService>();
            foundServiceMock.Setup(f => f.GetStocks()).Returns(new List<StockModel>());

            EventAggregator eventAggregator = new EventAggregator();

            var listViewModel = new ListViewModel(foundServiceMock.Object, eventAggregator);

            Assert.IsTrue(eventAggregator.GetEvent<StockAddedEvent>().Contains(listViewModel.RefreshData));
        }

        [TestMethod]
        public void RefreshDataShoudSetStocksProperty()
        {
            var stockModel = new BondModel();

            var foundServiceMock = new Mock<IFoundService>();
            foundServiceMock.Setup(f => f.GetStocks()).Returns(new List<StockModel>
            {
                stockModel
            });

            var listViewModel = new ListViewModel(foundServiceMock.Object, new EventAggregator());

            Assert.AreEqual(stockModel, listViewModel.Stocks.First());
        }

        #endregion
    }
}