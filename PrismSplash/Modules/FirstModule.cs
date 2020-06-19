using System;
using System.Threading;
using Microsoft.Practices.Composite.Modularity;

namespace PrismSplash.Modules
{
    public class FirstModule : IModule
    {
        public void Initialize()
        {
            Thread.Sleep(3000);
        }
    }
}