using System.Collections.Generic;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Ubs.Equities.Core.Models;
using Ubs.Equities.Core.Services;

namespace Ubs.Equities.Core.ViewModels
{
    public class ListViewModel : BindableBase, IListViewModel
    {
        #region Private fields

        private readonly IFoundService _foundService;

        private IEnumerable<StockModel> _stocks;

        #endregion
        #region Ctors

        public ListViewModel(IFoundService foundService, IEventAggregator eventAggregator)
        {
            _foundService = foundService;

            SubscribeStockAddedEvent(eventAggregator);

            RefreshData();
        }

        #endregion
        #region Internal methods

        internal virtual void RefreshData(string stockName = null)
        {
            Stocks = _foundService.GetStocks();
        }

        internal virtual void SubscribeStockAddedEvent(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<StockAddedEvent>().Subscribe(RefreshData, ThreadOption.BackgroundThread);
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