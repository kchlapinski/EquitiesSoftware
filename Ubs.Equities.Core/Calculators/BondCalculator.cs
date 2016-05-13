namespace Ubs.Equities.Core.Calculators
{
    public class BondCalculator : StockCalculator
    {
        public override decimal CalculateTransactionCost(decimal marketValue)
        {
            return marketValue * 0.02m;
        }
    }
}