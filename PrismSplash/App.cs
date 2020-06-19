using System;
using System.Windows;
using System.Windows.Threading;

namespace PrismSplash
{
    public class App : Application, ISplashApplication
    {
        private SplashWindow splash;

        public void ShowSplash()
        {
            splash = new SplashWindow(GetImagePath("SplashScreen.png"));
            splash.Show();
            splash.Activate();

            DoEvents();
        }

        public void HideSplash()
        {
            if (splash != null)
                splash.Close();
        }

        public void DoEvents()
        {
            ThreadDispatcher.Instance.DoEvents();
        }

        private void UpdateSplashMessageCore(object message)
        {
            splash.ProgressMessage = message.ToString();
        }

        public void UpdateSplashMessage(string message)
        {
            if (splash != null)
            {
                ThreadDispatcher.Instance.Invoke(DispatcherPriority.Background, UpdateSplashMessageCore, message);
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var bootstrapper = new Bootstrapper(this);

            bootstrapper.Run(true);
            MainWindow = bootstrapper.MainWindow;
            ShutdownMode = ShutdownMode.OnMainWindowClose;

            MainWindow.Show();

            base.OnStartup(e);
        }

        private string GetImagePath(string imageFilename)
        {
            return string.Format("pack://application:,,,/PrismSplash;Component/Images/{0}", imageFilename);
        }
    }
}
