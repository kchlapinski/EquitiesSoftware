namespace Ubs.Equities.Core.Calculators
{
    public class EquityCalculator : StockCalculator
    {
        public override decimal CalculateTransactionCost(decimal marketValue)
        {
            return marketValue * 0.05m;
        }
    }
}