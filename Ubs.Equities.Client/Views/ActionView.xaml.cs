using Ubs.Equities.Core.ViewModels;

namespace Ubs.Equities.Client.Views
{
    /// <summary>
    ///     Interaction logic for ActionView.xaml
    /// </summary>
    public partial class ActionView
    {
        #region Ctors

        public ActionView()
        {
            InitializeComponent();
        }

        public ActionView(IActionViewModel viewModel) : this()
        {
            DataContext = viewModel;
        }

        #endregion
    }
}