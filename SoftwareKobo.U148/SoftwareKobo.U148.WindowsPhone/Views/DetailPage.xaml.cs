using Brain.Animate;
using GalaSoft.MvvmLight.Messaging;
using SoftwareKobo.U148.Datas;
using SoftwareKobo.U148.Models;
using SoftwareKobo.U148.ViewModels;
using System;
using System.Threading.Tasks;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace SoftwareKobo.U148.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DetailPage : Page
    {
        private TaskCompletionSource<object> _isDomContentLoaded = new TaskCompletionSource<object>();

        public DetailPage()
        {
            this.InitializeComponent();
        }

        public DetailPageViewModel ViewModel
        {
            get
            {
                return (DetailPageViewModel)this.DataContext;
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;

            Messenger.Default.Unregister<Feed>(this, GetCommentClick);

            Messenger.Default.Unregister<string>(this, SetContent);
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;

            Messenger.Default.Register<Feed>(this, GetCommentClick);

            Messenger.Default.Register<string>(this, SetContent);

            if (Settings.Instance.ThemeMode == ThemeMode.Day)
            {
                webView.Navigate(new Uri("ms-appx-web:///Html/detail_day.html"));
            }
            else
            {
                webView.Navigate(new Uri("ms-appx-web:///Html/detail_night.html"));
            }

            ViewModel.SetFeed(e.Parameter as Feed);
        }

        private async void GetCommentClick(Feed feed)
        {
            await this.AnimateAsync(new FadeOutAnimation());
            Frame.Navigate(typeof(CommentPage), feed);
        }

        private async void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                e.Handled = true;
                await this.AnimateAsync(new FadeOutRightAnimation()
                {
                    SpeedRatio = 2
                });
                if (Frame.CanGoBack)
                {
                    Frame.GoBack();
                }
            }
        }

        private async void SetContent(string content)
        {
            // 等待 Dom 加载完成。
            await _isDomContentLoaded.Task;

            await webView.InvokeScriptAsync("setContent", new string[] { content });

            await webView.InvokeScriptAsync("removeImgLink", new string[] { });
        }

        private void WebView_DOMContentLoaded(WebView sender, WebViewDOMContentLoadedEventArgs args)
        {
            _isDomContentLoaded.SetResult(null);
        }
    }
}