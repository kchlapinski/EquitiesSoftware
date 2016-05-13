using System.Collections.Generic;
using Ubs.Equities.Core.Models;
using Ubs.Equities.EntityFramework;

namespace Ubs.Equities.Core.Services
{
    public interface IFoundService
    {
        #region Public methods

        void AddStock(string name, decimal price, long quantity, StockType type);

        long StockCount(StockType type);

        IEnumerable<StockModel> GetStocks();

        #endregion
    }
}