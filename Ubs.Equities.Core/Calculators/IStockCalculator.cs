namespace Ubs.Equities.Core.Calculators
{
    public interface IStockCalculator
    {
        #region Public methods

        decimal CalculateMarketValue(decimal price, long quantity);

        decimal CalculateStockWeight(decimal marketValue, decimal totalMarketValue);

        decimal CalculateTransactionCost(decimal marketValue);

        #endregion
    }
}