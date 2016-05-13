using Ubs.Equities.Core.ViewModels;

namespace Ubs.Equities.Client.Views
{
    /// <summary>
    ///     Interaction logic for ListView.xaml
    /// </summary>
    public partial class ListView
    {
        #region Ctors

        public ListView()
        {
            InitializeComponent();
        }

        public ListView(IListViewModel viewModel) : this()
        {
            DataContext = viewModel;
        }

        #endregion
    }
}