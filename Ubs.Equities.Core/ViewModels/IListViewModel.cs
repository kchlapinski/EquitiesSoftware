using System.Collections.Generic;
using Ubs.Equities.Core.Models;

namespace Ubs.Equities.Core.ViewModels
{
    public interface IListViewModel
    {
        #region Properties

        IEnumerable<StockModel> Stocks { get; }

        #endregion
    }
}