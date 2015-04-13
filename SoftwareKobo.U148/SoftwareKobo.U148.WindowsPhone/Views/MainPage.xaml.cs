using GalaSoft.MvvmLight.Messaging;
using SoftwareKobo.U148.Models;
using SoftwareKobo.U148.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Phone.UI.Input;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using WinRTXamlToolkit.Controls.Extensions;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace SoftwareKobo.U148.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPageViewModel ViewModel
        {
            get
            {
                return (MainPageViewModel)this.DataContext;
            }
        }

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.

            HardwareButtons.BackPressed += HardwareButtons_BackPressed;

            Messenger.Default.Register<ItemClickEventArgs>(this, FeedItemClick);
        }

        private int _backPressCount;

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            e.Handled = true;

            if (_backPressCount > 0)
            {
                // 再次按下后退键，退出。
                Application.Current.Exit();
            }

            _backPressCount++;

            DoubleAnimationUsingKeyFrames animation = new DoubleAnimationUsingKeyFrames();
            animation.KeyFrames.Add(new LinearDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.0d)),
                Value = 0.0d
            });
            animation.KeyFrames.Add(new LinearDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.2d)),
                Value = 0.0d
            });
            animation.KeyFrames.Add(new LinearDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.6d)),
                Value = 0.7d
            });
            animation.KeyFrames.Add(new LinearDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.9d)),
                Value = 1.0d
            });
            animation.KeyFrames.Add(new LinearDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2.0d)),
                Value = 1.0d
            });
            animation.KeyFrames.Add(new LinearDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(3.0d)),
                Value = 0.0d
            });

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(animation);
            Storyboard.SetTarget(animation, brdExit);
            Storyboard.SetTargetProperty(animation, "Opacity");

            // 显示退出提示。
            brdExit.Visibility = Visibility.Visible;

            storyboard.Completed += (storyboardCompletedSender, args) =>
            {
                // 重置后退键按下次数。
                _backPressCount = 0;
                storyboard.Stop();
                brdExit.Opacity = 0.0d;

                // 隐藏退出提示。
                brdExit.Visibility = Visibility.Collapsed;
            };
            storyboard.Begin();
        }

        private void FeedItemClick(ItemClickEventArgs args)
        {
            Frame.Navigate(typeof(DetailPage), args.ClickedItem);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;

            Messenger.Default.Unregister<ItemClickEventArgs>(this, FeedItemClick);
        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var header in _headers)
            {
                SetHeaderStyle(header);
            }
        }

        private List<Grid> _headers = new List<Grid>();

        private void PivotHeader_Loaded(object sender, RoutedEventArgs e)
        {
            Grid grid = (Grid)sender;
            _headers.Add(grid);
            SetHeaderStyle(grid);
        }

        private void SetHeaderStyle(Grid header)
        {
            var selectedFeed = (KeyValuePair<FeedCategory, IncrementalLoadingFeedCollection>)pivot.SelectedItem;
            var headerFeed = (KeyValuePair<FeedCategory, IncrementalLoadingFeedCollection>)header.DataContext;

            var textBlock = header.GetDescendantsOfType<TextBlock>().Single();
            var border = header.GetDescendantsOfType<Border>().Single();

            if (selectedFeed.Key == headerFeed.Key)
            {
                var u148ThemeBrush = (SolidColorBrush)Application.Current.Resources["U148ThemeBrush"];
                textBlock.Foreground = u148ThemeBrush;
                border.Background = u148ThemeBrush;
            }
            else
            {
                textBlock.Foreground = new SolidColorBrush(Colors.Black);
                border.Background = new SolidColorBrush(Colors.Transparent);
            }
        }
    }
}