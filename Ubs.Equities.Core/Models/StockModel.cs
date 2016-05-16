using Ubs.Equities.Core.Calculators;
using Ubs.Equities.EntityFramework;

namespace Ubs.Equities.Core.Models
{
    public abstract class StockModel : IStockModel
    {
        #region Properties

        public decimal FoundTotalMarketValue { get; set; }

        protected abstract IStockCalculator StockCalculator { get; }

        protected abstract decimal Tolerance { get; }

        #endregion
        #region IStockModel Members

        public bool IsValid => MarketValue >= 0 && TransactionCost < Tolerance;

        public decimal MarketValue => StockCalculator.CalculateMarketValue(Price, Quantity);

        public string Name { get; set; }

        public decimal Price { get; set; }

        public long Quantity { get; set; }

        public decimal StockWeight => StockCalculator.CalculateStockWeight(MarketValue, FoundTotalMarketValue);

        public decimal TransactionCost => StockCalculator.CalculateTransactionCost(MarketValue);

        public abstract StockType Type { get; }

        #endregion
    }
}