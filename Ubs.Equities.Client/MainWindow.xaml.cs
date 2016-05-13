using Autofac;
using Microsoft.Practices.Prism.Regions;
using Ubs.Equities.Client.Views;

namespace Ubs.Equities.Client
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        #region Ctors

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(IRegionManager regionManager, IContainer context) : this()
        {
            AttachRegions(regionManager, context);
        }

        #endregion
        #region Private methods

        private void AttachRegions(IRegionManager regionManager, IContainer context)
        {
            RegionManager.SetRegionManager(ListContent, regionManager);
            RegionManager.SetRegionName(ListContent, Constants.ListRegionName);
            regionManager.RegisterViewWithRegion(Constants.ListRegionName, context.Resolve<ListView>);

            RegionManager.SetRegionManager(ActionContent, regionManager);
            RegionManager.SetRegionName(ActionContent, Constants.ActionRegionName);
            regionManager.RegisterViewWithRegion(Constants.ActionRegionName, context.Resolve<ActionView>);

            RegionManager.SetRegionManager(SidebarContent, regionManager);
            RegionManager.SetRegionName(SidebarContent, Constants.SidebarRegionName);
            regionManager.RegisterViewWithRegion(Constants.SidebarRegionName, context.Resolve<SidebarView>);
        }

        #endregion
    }
}