using System;
using System.Threading;
using Microsoft.Practices.Composite.Modularity;

namespace PrismSplash.Modules
{
    public class SecondModule : IModule
    {
        public void Initialize()
        {
            Thread.Sleep(2000);
        }
    }
}