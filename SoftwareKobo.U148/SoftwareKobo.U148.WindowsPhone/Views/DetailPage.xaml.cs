using Brain.Animate;
using GalaSoft.MvvmLight.Messaging;
using SoftwareKobo.U148.Datas;
using SoftwareKobo.U148.Extensions;
using SoftwareKobo.U148.Models;
using SoftwareKobo.U148.ViewModels;
using System;
using System.Diagnostics;
using Windows.Phone.UI.Input;
using Windows.Storage;
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
        public DetailPageViewModel ViewModel
        {
            get
            {
                return (DetailPageViewModel)this.DataContext;
            }
        }

        public DetailPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;

            Messenger.Default.Register<Feed>(this,GetCommentClick);

            webView.Navigate(new Uri("ms-appx-web:///Html/detail.html"));
            await webView.WaitForDOMContentLoaded();

            ViewModel.SetFeed(e.Parameter as Feed);
        }

        private void GetCommentClick(Feed feed)
        {
            Frame.Navigate(typeof(CommentPage), feed);
        }
        
        private async void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                e.Handled = true;
                await AnimationTrigger.AnimateClose();
                Frame.GoBack();
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;

            Messenger.Default.Unregister<Feed>(this, GetCommentClick);
        }
    }
}