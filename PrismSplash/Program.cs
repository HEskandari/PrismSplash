using System;

namespace PrismSplash
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            var splashApp = new App();
            splashApp.Run();
        }
    }
}