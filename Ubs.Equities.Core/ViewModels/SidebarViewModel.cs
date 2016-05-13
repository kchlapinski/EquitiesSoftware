using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Ubs.Equities.Core.Models;
using Ubs.Equities.Core.Services;
using Ubs.Equities.EntityFramework;

namespace Ubs.Equities.Core.ViewModels
{
    internal class SidebarViewModel : BindableBase, ISidebarViewModel
    {
        #region Private fields

        private decimal _bondTotalMarketValue;

        private long _bondTotalNumber;

        private decimal _bondTotalWeight;

        private decimal _equityTotalMarketValue;

        private long _equityTotalNumber;

        private decimal _equityTotalWeight;

        private readonly IFoundService _foundService;

        #endregion
        #region Ctors

        public SidebarViewModel(IFoundService foundService, IEventAggregator eventAggregator)
        {
            _foundService = foundService;

            SubscribeStockAddedEvent(eventAggregator);

            RefreshData();
        }

        internal virtual void SubscribeStockAddedEvent(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<StockAddedEvent>().Subscribe(RefreshData, ThreadOption.BackgroundThread);
        }

        #endregion
        #region Private methods

        internal virtual void RefreshData(string stockName = null)
        {
            List<StockModel> stockModels = _foundService.GetStocks().ToList();

            BondTotalNumber = stockModels.Count(s => s.Type == StockType.Bond);
            BondTotalMarketValue = stockModels.Where(s => s.Type == StockType.Bond).Sum(s => s.MarketValue);
            BondTotalWeight = stockModels.Where(s => s.Type == StockType.Bond).Sum(s => s.StockWeight);

            EquityTotalNumber = stockModels.Count(s => s.Type == StockType.Equity);
            EquityTotalMarketValue = stockModels.Where(s => s.Type == StockType.Equity).Sum(s => s.MarketValue);
            EquityTotalWeight = stockModels.Where(s => s.Type == StockType.Equity).Sum(s => s.StockWeight);

            OnPropertyChanged("TotalNumber");
            OnPropertyChanged("TotalWeight");
            OnPropertyChanged("TotalMarketValue");
        }

        #endregion
        #region ISidebarViewModel Members
        public decimal BondTotalMarketValue
        {
            get
            {
                return _bondTotalMarketValue;
            }
            private set
            {
                SetProperty(ref _bondTotalMarketValue, value);
            }
        }

        public long BondTotalNumber
        {
            get
            {
                return _bondTotalNumber;
            }
            private set
            {
                SetProperty(ref _bondTotalNumber, value);
            }
        }

        public decimal BondTotalWeight
        {
            get
            {
                return _bondTotalWeight;
            }
            private set
            {
                SetProperty(ref _bondTotalWeight, value);
            }
        }

        public decimal EquityTotalMarketValue
        {
            get
            {
                return _equityTotalMarketValue;
            }
            private set
            {
                SetProperty(ref _equityTotalMarketValue, value);
            }
        }

        public long EquityTotalNumber
        {
            get
            {
                return _equityTotalNumber;
            }
            private set
            {
                SetProperty(ref _equityTotalNumber, value);
            }
        }

        public decimal EquityTotalWeight
        {
            get
            {
                return _equityTotalWeight;
            }
            private set
            {
                SetProperty(ref _equityTotalWeight, value);
            }
        }

        public decimal TotalMarketValue => BondTotalMarketValue + EquityTotalMarketValue;

        public long TotalNumber => BondTotalNumber + EquityTotalNumber;

        public decimal TotalWeight => BondTotalWeight + EquityTotalWeight;

        #endregion
    }
}