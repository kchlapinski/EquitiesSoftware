using Ubs.Equities.Core.Calculators;
using Ubs.Equities.EntityFramework;

namespace Ubs.Equities.Core.Models
{
    public class EquityModel : StockModel
    {
        #region Properties

        protected override IStockCalculator StockCalculator => new EquityCalculator();

        protected override decimal Tolerance => 200000;

        public override StockType Type => StockType.Equity;

        #endregion
    }
}