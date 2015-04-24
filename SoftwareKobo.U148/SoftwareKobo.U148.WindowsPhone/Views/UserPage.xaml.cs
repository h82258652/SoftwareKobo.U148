using Brain.Animate;
using SoftwareKobo.U148.ViewModels;
using System;
using Windows.Phone.UI.Input;
using Windows.System;
using Windows.UI.Xaml;
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
            e.Handled = true;
            GoBack();
        }

        private async void GoBack()
        {
            if (Frame.CanGoBack)
            {
                await AnimationTrigger.AnimateClose();
                if (Frame.CanGoBack)
                {
                    Frame.GoBack();
                }
            }
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtEmail.Text.Length <= 0)
            {
                txtError.Text = "邮箱不能为空";
                txtEmail.Focus(FocusState.Programmatic);
                return;
            }
            if (pwdPassword.Password.Length <= 0)
            {
                txtError.Text = "密码不能为空";
                pwdPassword.Focus(FocusState.Programmatic);
                return;
            }

            string result = await ViewModel.Login(txtEmail.Text, pwdPassword.Password);
            if (result == "success")
            {
                GoBack();
            }
            else
            {
                txtError.Text = result;
                return;
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Logout();
            GoBack();
        }

        private async void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("http://www.u148.net/user/register.html", UriKind.Absolute));
        }
    }
}