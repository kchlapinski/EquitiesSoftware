using Ubs.Equities.Core.ViewModels;

namespace Ubs.Equities.Client.Views
{
    /// <summary>
    ///     Interaction logic for SidebarView.xaml
    /// </summary>
    public partial class SidebarView
    {
        #region Ctors

        public SidebarView()
        {
            InitializeComponent();
        }

        public SidebarView(ISidebarViewModel viewModel) : this()
        {
            DataContext = viewModel;
        }

        #endregion
    }
}