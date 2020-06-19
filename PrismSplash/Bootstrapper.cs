using System;
using System.Windows;
using Composite.WindsorExtensions;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Logging;
using Microsoft.Practices.Composite.Modularity;
using PrismSplash.Modules;

namespace PrismSplash
{
    public class Bootstrapper : WindsorBootstrapper
    {
        private ISplashApplication splashApp;

        public Bootstrapper(ISplashApplication splashApp)
        {
            this.splashApp = splashApp;
        }

        protected override void InitializeModules()
        {
            this.UpdateMessage("Initializing modules...");
            this.SubscribeModuleLoadingEvent();
            base.InitializeModules();
        }

        protected override void ConfigureContainer()
        {
            Container.RegisterType<IModuleLoader, SplashModuleLoader>(true);

            base.ConfigureContainer();
        }

        protected override IModuleEnumerator GetModuleEnumerator()
        {
            var moduleEnumerator = new StaticModuleEnumerator();

            moduleEnumerator.AddModule(typeof (FirstModule));
            moduleEnumerator.AddModule(typeof (SecondModule));
            moduleEnumerator.AddModule(typeof (ThirdModule));

            return moduleEnumerator;
        }

        private void SubscribeModuleLoadingEvent()
        {
            SplashModuleLoader loader = (SplashModuleLoader)Container.Resolve(typeof(IModuleLoader));
            loader.ModuleInitializing += LoadingModule;
        }

        private void LoadingModule(object sender, DataEventArgs<Type> e)
        {
            UpdateMessage("Loading module [" + e.Value.Name + "]");
        }

        public override void Run(bool useDefaultConfiguration)
        {
            this.splashApp.ShowSplash();
            this.UpdateMessage("Initializing bootstrapper");

            base.Run(useDefaultConfiguration);
            this.UpdateMessage("Bootstrapper sequence completed");

            this.splashApp.HideSplash();

        }

        protected override DependencyObject CreateShell()
        {
            MainWindow = new Window();
            return MainWindow;
        }

        private void UpdateMessage(string msg)
        {
            base.LoggerFacade.Log(msg, Category.Info, Priority.Low);
            splashApp.UpdateSplashMessage(msg);
        }

        public Window MainWindow
        {
            get; set;
        }
    }
}