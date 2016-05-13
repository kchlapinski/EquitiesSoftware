namespace Ubs.Equities.Core.ViewModels
{
    public interface ISidebarViewModel
    {
        #region Properties

        decimal BondTotalMarketValue { get; }

        long BondTotalNumber { get; }

        decimal BondTotalWeight { get; }

        decimal EquityTotalMarketValue { get; }

        long EquityTotalNumber { get; }

        decimal EquityTotalWeight { get; }

        decimal TotalMarketValue { get; }

        long TotalNumber { get; }

        decimal TotalWeight { get; }

        #endregion
    }
}