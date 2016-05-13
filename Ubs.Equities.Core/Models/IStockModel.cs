using Ubs.Equities.EntityFramework;

namespace Ubs.Equities.Core.Models
{
    public interface IStockModel
    {
        #region Properties

        bool IsValid { get; }

        decimal MarketValue { get; }

        string Name { get; }

        decimal Price { get; }

        long Quantity { get; }

        decimal StockWeight { get; }

        decimal TransactionCost { get; }

        StockType Type { get; }

        #endregion
    }
}