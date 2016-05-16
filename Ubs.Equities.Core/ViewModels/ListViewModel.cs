using System.Collections.Generic;
using Microsoft.Practices.Prism.PubSubEvents;
using Ubs.Equities.Core.Models;
using Ubs.Equities.Core.Services;

namespace Ubs.Equities.Core.ViewModels
{
    public class ListViewModel : ViewModelBase, IListViewModel
    {
        #region Private fields

        private IEnumerable<StockModel> _stocks;

        #endregion
        #region Ctors

        public ListViewModel(IFoundService foundService, IEventAggregator eventAggregator) : base(foundService, eventAggregator)
        {
        }

        #endregion
        #region Overrides

        protected override void InitializeViewModel()
        {
            SubscribeStockAddedEvent();

            RefreshData();
        }

        #endregion
        #region Internal methods

        internal virtual void RefreshData(string stockName = null)
        {
            Stocks = FoundService.GetStocks();
        }

        internal virtual void SubscribeStockAddedEvent()
        {
            EventAggregator.GetEvent<StockAddedEvent>().Subscribe(RefreshData, ThreadOption.BackgroundThread);
        }

        #endregion
        #region IListViewModel Members

        public IEnumerable<StockModel> Stocks
        {
            get
            {
                return _stocks;
            }
            private set
            {
                SetProperty(ref _stocks, value);
            }
        }

        #endregion
    }
}