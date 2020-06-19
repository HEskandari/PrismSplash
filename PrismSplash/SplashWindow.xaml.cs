using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PrismSplash
{
    /// <summary>
    /// Interaction logic for SplashWindow.xaml
    /// </summary>
    public partial class SplashWindow : INotifyPropertyChanged
    {


        #region Ctor

        public SplashWindow()
        {
            InitializeComponent();
        }

        public SplashWindow(string imageUri) : this()
        {
            this.BackgroundImageUri = imageUri;
            var uriSource = new Uri(this.BackgroundImageUri);
            var image = new BitmapImage(uriSource);

            this.Left = ((SystemParameters.FullPrimaryScreenWidth - image.Width) / 2);
            this.Top = ((SystemParameters.FullPrimaryScreenHeight - image.Height) / 2);
            this.Background = new SolidColorBrush(Colors.Transparent);
            this.SplashImage.Source = image;
            this.DataContext = this;
        }

        #endregion

        #region Props

        private string _backgroundImageUri;
        public string BackgroundImageUri
        {
            get { return _backgroundImageUri; }
            set
            {
                _backgroundImageUri = value;
                PropertyChanged(this, new PropertyChangedEventArgs("BackgroundImageUri"));
            }
        }

        public string ProgressMessage
        {
            get { return lblMessage.Text; }
            set
            {
                lblMessage.Text = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ProgressMessage"));
            }
        }

        #endregion

        #region Methods

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            var handle = new WindowInteropHelper(this).Handle;
            var dwNewLong = UnsafeNativeMethods.GetWindowLong(handle, -20) | UnsafeNativeMethods.WS_EX_TOOLWINDOW;
            UnsafeNativeMethods.SetWindowLong(handle, UnsafeNativeMethods.GWL_EXSTYLE, dwNewLong);
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
