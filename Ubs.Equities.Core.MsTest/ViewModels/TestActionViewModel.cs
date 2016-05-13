using System.Threading.Tasks;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using Ubs.Equities.Core.Services;
using Ubs.Equities.Core.ViewModels;
using Ubs.Equities.EntityFramework;

namespace Ubs.Equities.Core.MsTest.ViewModels
{
    [TestClass]
    public class TestActionViewModel
    {
        #region Public methods

        [TestMethod]
        public void ActionViewModelShouldBeInvalidWhenPriceIsEmpty()
        {
            var actionViewModel = new ActionViewModel(null, null)
            {
                Quantity = 1,
                StockType = StockType.Bond
            };

            Assert.IsFalse(actionViewModel.IsValid());
        }

        [TestMethod]
        public void ActionViewModelShouldBeInvalidWhenPriceOrQuantityAreEmpty()
        {
            var actionViewModel = new ActionViewModel(null, null)
            {
                StockType = StockType.Bond
            };

            Assert.IsFalse(actionViewModel.IsValid());
        }

        [TestMethod]
        public void ActionViewModelShouldBeInvalidWhenQuantityIsEmpty()
        {
            var actionViewModel = new ActionViewModel(null, null)
            {
                Price = 10m,
                StockType = StockType.Bond
            };

            Assert.IsFalse(actionViewModel.IsValid());
        }

        [TestMethod]
        public void ActionViewModelShouldBeValidWhenPriceOrQuantityAreEmpty()
        {
            var actionViewModel = new ActionViewModel(null, null)
            {
                Price = 10m,
                Quantity = 1,
                StockType = StockType.Bond
            };

            Assert.IsTrue(actionViewModel.IsValid());
        }

        [TestMethod]
        public void AddStockCommandShouldBeAvailableWhenViewModelIsValid()
        {
            var actionViewModel = new ActionViewModel(null, null)
            {
                Price = 10m,
                Quantity = 1,
                StockType = StockType.Bond
            };

            Assert.IsTrue(actionViewModel.AddStockCommand.CanExecute());
        }

        [TestMethod]
        public async Task AddStockCommandShouldInvokeAddStockAction()
        {
            var foundServiceMock = new Mock<IFoundService>();

            foundServiceMock.Setup(f => f.StockCount(It.IsAny<StockType>())).Returns(4);
            foundServiceMock.Setup(f => f.AddStock(It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<long>(), It.IsAny<StockType>()));

            var eventAggregatorMock = new Mock<IEventAggregator>();
            eventAggregatorMock.Setup(e => e.GetEvent<StockAddedEvent>()).Returns(new StockAddedEvent());

            var actionViewModelMock = new Mock<ActionViewModel>(foundServiceMock.Object, eventAggregatorMock.Object)
            {
                CallBase = true
            };

            actionViewModelMock.Object.StockType = StockType.Bond;

            await actionViewModelMock.Object.AddStockCommand.Execute();

            foundServiceMock.VerifyAll();
            eventAggregatorMock.VerifyAll();
        }

        [TestMethod]
        public void AddStockCommandShouldNotBeAvailableWhenViewModelIsInvalid()
        {
            var actionViewModel = new ActionViewModel(null, null);

            Assert.IsFalse(actionViewModel.AddStockCommand.CanExecute());
        }

        [TestMethod]
        public void AddStockCommandShouldNotBeNullAfterInitializationOfActionViewModel()
        {
            var actionViewModel = new ActionViewModel(null, null);

            Assert.IsNotNull(actionViewModel.AddStockCommand);
        }

        [TestMethod]
        public void GetNameShouldReturnTypeSuffixedWithCorrectedNumber()
        {
            var foundServiceMock = new Mock<IFoundService>();

            foundServiceMock.Setup(f => f.StockCount(It.IsAny<StockType>())).Returns(4);

            var actionViewModel = new ActionViewModel(foundServiceMock.Object, null)
            {
                StockType = StockType.Bond
            };

            Assert.AreEqual("Bond5", actionViewModel.GetStockName());
        }

        [TestMethod]
        public void SettingPricePropertyShouldInvokeOnceRaiseAddStockCanExecuteChanged()
        {
            const string MethodName = "RaiseAddStockCanExecuteChanged";

            var mock = new Mock<ActionViewModel>(null, null)
            {
                CallBase = true
            };

            mock.Protected().Setup(MethodName);

            mock.Object.Price = 10m;

            mock.Protected().Verify(MethodName, Times.Once());
        }

        [TestMethod]
        public void SettingQuantityPropertyShouldInvokeOnceRaiseAddStockCanExecuteChanged()
        {
            const string MethodName = "RaiseAddStockCanExecuteChanged";

            var mock = new Mock<ActionViewModel>(null, null)
            {
                CallBase = true
            };

            mock.Protected().Setup(MethodName);

            mock.Object.Quantity = 1;

            mock.Protected().Verify(MethodName, Times.Once());
        }

        #endregion
    }
}