using System.ComponentModel;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Ubs.Equities.Core.Services;
using Ubs.Equities.EntityFramework;

namespace Ubs.Equities.Core.ViewModels
{
    public class ActionViewModel : BindableBase, IDataErrorInfo, IActionViewModel
    {
        #region Private fields

        private readonly IEventAggregator _eventAggregator;

        private readonly IFoundService _foundService;

        private decimal _price;

        private long _quantity;

        private StockType _stockType;

        #endregion
        #region Ctors

        public ActionViewModel(IFoundService foundService, IEventAggregator eventAggregator)
        {
            _foundService = foundService;
            _eventAggregator = eventAggregator;

            AddStockCommand = new DelegateCommand(AddStockAction, IsValid);
        }

        #endregion
        #region Protected methods

        protected virtual void RaiseAddStockCanExecuteChanged()
        {
            AddStockCommand.RaiseCanExecuteChanged();
        }

        #endregion
        #region Internal methods

        internal string GetStockName()
        {
            long count = _foundService.StockCount(StockType);

            string name = $"{StockType}{count + 1}";
            return name;
        }

        internal bool IsValid()
        {
            var dataErrorInfo = (IDataErrorInfo)this;

            return string.IsNullOrEmpty(dataErrorInfo[nameof(Price)]) && string.IsNullOrEmpty(dataErrorInfo[nameof(Quantity)]);
        }

        #endregion
        #region Private methods

        private void AddStockAction()
        {
            string name = GetStockName();

            _foundService.AddStock(name, Price, Quantity, StockType);

            PublishStockAddedEvent(name);
        }

        private void PublishStockAddedEvent(string name)
        {
            _eventAggregator.GetEvent<StockAddedEvent>().Publish(name);
        }

        #endregion
        #region IActionViewModel Members

        public DelegateCommand AddStockCommand { get; }

        public decimal Price
        {
            get
            {
                return _price;
            }
            set
            {
                SetProperty(ref _price, value);
                RaiseAddStockCanExecuteChanged();
            }
        }

        public long Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                SetProperty(ref _quantity, value);
                RaiseAddStockCanExecuteChanged();
            }
        }

        public StockType StockType
        {
            get
            {
                return _stockType;
            }
            set
            {
                SetProperty(ref _stockType, value);
            }
        }

        #endregion
        #region IDataErrorInfo Members

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                string errorMessage = string.Empty;
                switch (columnName)
                {
                    case "Price":
                        if (Price == 0)
                        {
                            errorMessage = "Price cannot be equal to zero";
                        }
                        break;

                    case "Quantity":
                        if (Quantity == 0)
                        {
                            errorMessage = "Quantity has to be greater than zero";
                        }
                        break;
                }

                return errorMessage;
            }
        }

        string IDataErrorInfo.Error => null;

        #endregion
    }
}