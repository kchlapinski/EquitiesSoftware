using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Ubs.Equities.Core.Services;

namespace Ubs.Equities.Core.ViewModels
{
    public class ViewModelBase : BindableBase
    {
        #region Properties

        protected IEventAggregator EventAggregator { get; }

        protected IFoundService FoundService { get; }

        #endregion
        #region Ctors

        public ViewModelBase(IFoundService foundService, IEventAggregator eventAggregator)
        {
            FoundService = foundService;
            EventAggregator = eventAggregator;

            Initialize();
        }

        #endregion
        #region Protected methods

        protected virtual void InitializeViewModel()
        {
        }

        #endregion
        #region Private methods

        private void Initialize()
        {
            InitializeViewModel();
        }

        #endregion
    }
}