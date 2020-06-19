using System;
using System.Threading;
using Microsoft.Practices.Composite.Modularity;

namespace PrismSplash.Modules
{
    public class ThirdModule : IModule
    {
        public void Initialize()
        {
            Thread.Sleep(1000);
        }
    }
}