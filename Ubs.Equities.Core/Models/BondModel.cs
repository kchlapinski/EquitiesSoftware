using Ubs.Equities.Core.Calculators;
using Ubs.Equities.EntityFramework;

namespace Ubs.Equities.Core.Models
{
    public class BondModel : StockModel
    {
        #region Properties

        protected override IStockCalculator StockCalculator => new BondCalculator();

        protected override decimal Tolerance => 100000;

        public override StockType Type => StockType.Bond;

        #endregion
    }
}