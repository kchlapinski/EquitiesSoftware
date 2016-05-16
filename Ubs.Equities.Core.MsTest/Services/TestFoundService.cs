using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Autofac.Features.Indexed;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ubs.Equities.Core.Calculators;
using Ubs.Equities.Core.Models;
using Ubs.Equities.Core.Services.Implementors;
using Ubs.Equities.EntityFramework;
using Ubs.Equities.EntityFramework.DbContexts;
using Ubs.Equities.EntityFramework.Entities;

namespace Ubs.Equities.Core.MsTest.Services
{
    [TestClass]
    public class TestFoundService
    {
        #region Public methods

        [TestMethod]
        public void AddStockHasToAddStockToDbSet()
        {
            Found found = CreateDefaultFound();
            List<Found> founds = CreateList(found);
            Mock<IDbSet<Found>> dbSetFoundsMock = CreateDbSetMock(founds);

            List<Stock> stocks = CreateList<Stock>();
            Mock<IDbSet<Stock>> dbSetStocksMock = CreateDbSetMock(stocks);

            var foundDbContext = new Mock<IFoundDbContext>();
            foundDbContext.Setup(f => f.Founds).Returns(dbSetFoundsMock.Object);
            foundDbContext.Setup(f => f.Stocks).Returns(dbSetStocksMock.Object);

            var foundService = new FoundService(foundDbContext.Object, null, null);

            foundService.AddStock("Bond1", 10m, 1, StockType.Bond);

            Stock stock = foundDbContext.Object.Stocks.FirstOrDefault();

            Assert.IsNotNull(stock);
            Assert.AreEqual("Bond1", stock.Name);
        }

        [TestMethod]
        public void DefaultFoundShouldBeAddedToDbSet()
        {
            List<Found> founds = CreateList<Found>();
            Mock<IDbSet<Found>> dbSetFoundsMock = CreateDbSetMock(founds);

            var foundDbContext = new Mock<IFoundDbContext>();
            foundDbContext.Setup(f => f.Founds).Returns(dbSetFoundsMock.Object);

            var foundService = new FoundService(foundDbContext.Object, null, null);

            Assert.IsNotNull(foundService.CurrentFound);
        }

        [TestMethod]
        public void GetStocksShouldReturnApproprietModels()
        {
            Found found = CreateDefaultFound();
            List<Found> founds = CreateList(found);
            Mock<IDbSet<Found>> dbSetFoundsMock = CreateDbSetMock(founds);

            Stock stock = CreateBondStock();
            List<Stock> stocks = CreateList(stock);
            Mock<IDbSet<Stock>> dbSetStocksMock = CreateDbSetMock(stocks);

            var foundDbContext = new Mock<IFoundDbContext>();
            foundDbContext.Setup(f => f.Founds).Returns(dbSetFoundsMock.Object);
            foundDbContext.Setup(f => f.Stocks).Returns(dbSetStocksMock.Object);

            var iindexMock = new Mock<IIndex<StockType, StockModel>>();
            iindexMock.SetupGet(i => i[It.IsAny<StockType>()]).Returns(new BondModel(new BondCalculator()));

            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map(It.IsAny<Stock>(), It.IsAny<StockModel>(), It.IsAny<Type>(), It.IsAny<Type>()))
                .Returns<Stock, StockModel, Type, Type>((s, m, st, sm) => m);

            var foundService = new FoundService(foundDbContext.Object, iindexMock.Object, mapper.Object);

            List<StockModel> stockModels = foundService.GetStocks().ToList();

            Assert.IsNotNull(stockModels);
            Assert.AreEqual(1, stockModels.Count(s => s.Type == StockType.Bond));
        }

        [TestMethod]
        public void StockCountHasToReturnCountOfObject()
        {
            Found found = CreateDefaultFound();
            List<Found> founds = CreateList(found);
            Mock<IDbSet<Found>> dbSetFoundsMock = CreateDbSetMock(founds);

            Stock stock = CreateBondStock();
            List<Stock> stocks = CreateList(stock);
            Mock<IDbSet<Stock>> dbSetStocksMock = CreateDbSetMock(stocks);

            var foundDbContext = new Mock<IFoundDbContext>();
            foundDbContext.Setup(f => f.Founds).Returns(dbSetFoundsMock.Object);
            foundDbContext.Setup(f => f.Stocks).Returns(dbSetStocksMock.Object);

            var foundService = new FoundService(foundDbContext.Object, null, null);

            Assert.AreEqual(1, foundService.StockCount(StockType.Bond));
            Assert.AreEqual(0, foundService.StockCount(StockType.Equity));
        }

        #endregion
        #region Private methods

        private static Stock CreateBondStock()
        {
            var stock = new Stock
            {
                FoundId = Guid.Empty,
                Name = "Bond1",
                Price = 10m,
                Quantity = 1,
                Type = StockType.Bond
            };

            return stock;
        }

        private static Mock<IDbSet<T>> CreateDbSetMock<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();
            var dbSetMock = new Mock<IDbSet<T>>();

            dbSetMock.Setup(m => m.Provider).Returns(queryable.Provider);
            dbSetMock.Setup(m => m.Expression).Returns(queryable.Expression);
            dbSetMock.Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSetMock.Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
            dbSetMock.Setup(d => d.Add(It.IsAny<T>())).Callback<T>(sourceList.Add);

            return dbSetMock;
        }

        private static Found CreateDefaultFound()
        {
            var found = new Found
            {
                Id = Guid.Empty,
                Name = "MyFound"
            };

            return found;
        }

        private static List<T> CreateList<T>(T entity = null) where T : class
        {
            var list = new List<T>();

            if (entity != null)
            {
                list.Add(entity);
            }

            return list;
        }

        #endregion
    }
}