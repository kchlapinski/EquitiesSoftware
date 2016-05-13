namespace Ubs.Equities.Core.Calculators
{
    public abstract class StockCalculator : IStockCalculator
    {
        #region ICalculator Members

        public decimal CalculateMarketValue(decimal price, long quantity)
        {
            return price * quantity;
        }

        public decimal CalculateStockWeight(decimal marketValue, decimal totalMarketValue)
        {
            return marketValue * 100 / totalMarketValue;
        }

        public abstract decimal CalculateTransactionCost(decimal marketValue);

        #endregion
    }
}