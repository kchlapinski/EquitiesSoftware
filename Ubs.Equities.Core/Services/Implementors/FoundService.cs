using System.Collections.Generic;
using System.Linq;
using Autofac.Features.Indexed;
using AutoMapper;
using Ubs.Equities.Core.Models;
using Ubs.Equities.EntityFramework;
using Ubs.Equities.EntityFramework.DbContexts;
using Ubs.Equities.EntityFramework.Entities;

namespace Ubs.Equities.Core.Services.Implementors
{
    internal class FoundService : IFoundService
    {
        #region Private fields

        private Found _currentFound;

        private readonly IFoundDbContext _foundDbContext;

        private readonly IMapper _mapper;

        private readonly IIndex<StockType, StockModel> _stockModelFactory;

        #endregion
        #region Properties

        internal Found CurrentFound => _currentFound ?? (_currentFound = GetCurrentFound());

        #endregion
        #region Ctors

        public FoundService(IFoundDbContext foundDbContext, IIndex<StockType, StockModel> stockModelFactory, IMapper mapper)
        {
            _foundDbContext = foundDbContext;
            _stockModelFactory = stockModelFactory;
            _mapper = mapper;
        }

        #endregion
        #region Private methods

        private Found CreateDefaultFound()
        {
            var found = new Found
            {
                Name = "MyFound"
            };

            _foundDbContext.Founds.Add(found);
            _foundDbContext.SaveChanges();

            return found;
        }

        private Found GetCurrentFound()
        {
            return _foundDbContext.Founds.FirstOrDefault() ?? CreateDefaultFound();
        }

        #endregion
        #region IFoundService Members

        public void AddStock(string name, decimal price, long quantity, StockType type)
        {
            _foundDbContext.Stocks.Add(new Stock
            {
                FoundId = CurrentFound.Id,
                Name = name,
                Price = price,
                Quantity = quantity,
                Type = type
            });

            _foundDbContext.SaveChanges();
        }

        public long StockCount(StockType type)
        {
            return _foundDbContext.Stocks.Count(s => s.FoundId == CurrentFound.Id && s.Type == type);
        }

        public IEnumerable<StockModel> GetStocks()
        {
            List<Stock> stocks = _foundDbContext.Stocks.Where(s => s.FoundId == CurrentFound.Id).ToList();
            var totalMarketValue = stocks.Sum(s => s.Quantity * s.Price);

            return stocks.Select(stock =>
            {
                StockModel stockModel = _stockModelFactory[stock.Type];
                stockModel.FoundTotalMarketValue = totalMarketValue;

                return (StockModel)_mapper.Map(stock, stockModel, typeof(Stock), stockModel.GetType());
            });
        }

        #endregion
    }
}