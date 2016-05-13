using Microsoft.Practices.Prism.Commands;
using Ubs.Equities.EntityFramework;

namespace Ubs.Equities.Core.ViewModels
{
    public interface IActionViewModel
    {
        #region Properties

        DelegateCommand AddStockCommand { get; }

        decimal Price { get; set; }

        long Quantity { get; set; }

        StockType StockType { get; set; }

        #endregion
    }
}