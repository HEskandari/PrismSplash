namespace PrismSplash
{
    public interface ISplashApplication
    {
        void ShowSplash();
        void HideSplash();
        void DoEvents();
        void UpdateSplashMessage(string message);
    }
}