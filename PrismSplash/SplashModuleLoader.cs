using System;
using Microsoft.Practices.Composite;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Logging;
using Microsoft.Practices.Composite.Modularity;

namespace PrismSplash
{
    public class SplashModuleLoader : ModuleLoader
    {
        public event EventHandler<DataEventArgs<Type>> ModuleInitializing;

        public SplashModuleLoader(IContainerFacade containerFacade, ILoggerFacade loggerFacade) : base(containerFacade, loggerFacade)
        {
            this.Container = containerFacade;
            this.Logger = loggerFacade;
        }

        protected override IModule CreateModule(Type type)
        {
            IModule module = Container.Resolve(type) as IModule;
            OnModuleInitializing(type);

            return module;
        }

        protected virtual void OnModuleInitializing(Type type)
        {
            var eventHandler = ModuleInitializing;

            if(eventHandler != null)
            {
                eventHandler(this, new DataEventArgs<Type>(type));
            }
        }

        private IContainerFacade Container
        { 
            get; set;
        }

        private ILoggerFacade Logger
        {
            get; set;
        }
    }
}