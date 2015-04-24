using SoftwareKobo.U148.ViewModels;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace SoftwareKobo.U148.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserPage : Page
    {
        public UserPageViewModel ViewModel
        {
            get
            {
                return (UserPageViewModel)this.DataContext;
            }
        }

        public UserPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                e.Handled = true;
                Frame.GoBack();
            }
        }

        private async void btnLogin_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            string result = await ViewModel.Login(txtEmail.Text, pwdPassword.Password);
            if (result == "success")
            {
            }
            else
            {
            }
        }

        private void btnLogout_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }
    }
}