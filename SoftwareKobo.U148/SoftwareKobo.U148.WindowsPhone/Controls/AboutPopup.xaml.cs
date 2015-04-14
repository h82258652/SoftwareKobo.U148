using SoftwareKobo.U148.Controls.Interfaces;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SoftwareKobo.U148.Controls
{
    public sealed partial class AboutPopup : UserControl
    {
        public AboutPopup()
        {
            this.InitializeComponent();
        }

        public bool IsOpen
        {
            get
            {
                return Visibility == Visibility.Visible;
            }
        }

        public void Show()
        {
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;

            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame != null)
            {
                Page page = rootFrame.Content as Page;
                if (page != null)
                {
                    AppBar appBar = page.BottomAppBar;
                    if (appBar != null)
                    {
                        appBar.Visibility = Visibility.Collapsed;
                    }

                    IApplicationBar customAppBarPage = page as IApplicationBar;
                    if (customAppBarPage != null)
                    {
                        customAppBarPage.ApplicationBar.Visibility = Visibility.Collapsed;
                    }
                }
            }

            showStoryboard.Begin();
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            e.Handled = true;
            Hide();
        }

        public void Hide()
        {
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;

            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame != null)
            {
                Page page = rootFrame.Content as Page;
                if (page != null)
                {
                    AppBar appBar = page.BottomAppBar;
                    if (appBar != null)
                    {
                        appBar.Visibility = Visibility.Visible;
                    }

                    IApplicationBar customAppBarPage = page as IApplicationBar;
                    if (customAppBarPage != null)
                    {
                        customAppBarPage.ApplicationBar.Visibility = Visibility.Visible;
                    }
                }
            }

            hideStoryboard.Begin();
        }

        private void brdClose_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Hide();
        }
    }
}