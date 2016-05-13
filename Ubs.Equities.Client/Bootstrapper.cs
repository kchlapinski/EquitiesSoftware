using System.Windows;
using Autofac;
using Microsoft.Practices.Prism.Regions;
using Prism.AutofacExtension;
using Ubs.Equities.Client.Views;
using Ubs.Equities.Core;
using Ubs.Equities.Core.ViewModels;

namespace Ubs.Equities.Client
{
    public class Bootstrapper : AutofacBootstrapper
    {
        #region Overrides

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new CoreModule());

            builder.Register(context => new ActionView(context.Resolve<IActionViewModel>()));

            builder.Register(context => new ListView(context.Resolve<IListViewModel>()));

            builder.Register(context => new SidebarView(context.Resolve<ISidebarViewModel>()));

            builder.Register(context => new MainWindow(context.Resolve<IRegionManager>(), Container));

            base.ConfigureContainer(builder);
        }

        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        #endregion
    }
}