using System;
using System.Windows;

namespace Ubs.Equities.Client
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        #region Overrides

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            AppDomain.CurrentDomain.UnhandledException += AppDomainOnUnhandledException;

            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }

        private void AppDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        {
            //TODO: add some scenario to handle exceptions
        }

        #endregion
    }
}