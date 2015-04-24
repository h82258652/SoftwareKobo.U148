using Brain.Animate;
using SoftwareKobo.U148.Controls;
using SoftwareKobo.U148.Controls.Interfaces;
using SoftwareKobo.U148.DataModels;
using SoftwareKobo.U148.Datas;
using SoftwareKobo.U148.Extensions;
using SoftwareKobo.U148.Models;
using SoftwareKobo.U148.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.ApplicationModel.Email;
using Windows.ApplicationModel.Store;
using Windows.Phone.UI.Input;
using Windows.System;
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
    public sealed partial class MainPage : Page, IApplicationBar
    {
        private int _backPressCount;

        private List<Grid> _headers = new List<Grid>();

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        public ApplicationBar ApplicationBar
        {
            get
            {
                return appBar;
            }
        }

        public MainPageViewModel ViewModel
        {
            get
            {
                return (MainPageViewModel)this.DataContext;
            }
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
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.

            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            // 将日间模式变夜间模式，夜间模式变日间模式。
            Settings.Instance.ThemeMode = 1 - Settings.Instance.ThemeMode;

            // 重新设定 Pivot 头部的样式。
            foreach (var header in _headers)
            {
                SetHeaderStyle(header);
            }
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            appBar.IsOpen = false;
            ctlAbout.Show();
        }

        private async void btnGivePraise_Click(object sender, RoutedEventArgs e)
        {
            appBar.IsOpen = false;
            await Launcher.LaunchUriAsync(new Uri("ms-windows-store:reviewapp?appid=" + CurrentApp.AppId));
        }

        private void btnSwitchTheme_Click(object sender, RoutedEventArgs e)
        {
            appBar.IsOpen = false;

            // 将日间模式变夜间模式，夜间模式变日间模式。
            Settings.Instance.ThemeMode = 1 - Settings.Instance.ThemeMode;

            // 重新设定 Pivot 头部的样式。
            foreach (var header in _headers)
            {
                SetHeaderStyle(header);
            }
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            e.Handled = true;

            if (appBar.IsOpen)
            {
                return;
            }

            if (ctlAbout.IsOpen)
            {
                return;
            }

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

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var header in _headers)
            {
                SetHeaderStyle(header);
            }
        }

        private void PivotHeader_Loaded(object sender, RoutedEventArgs e)
        {
            // 缓存所有 Pivot 头。

            Grid grid = (Grid)sender;
            _headers.Add(grid);
            SetHeaderStyle(grid);
        }

        private async void PullToRefreshListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // 显示页面离开动画。
            await AnimationTrigger.AnimateClose();
            Frame.Navigate(typeof(DetailPage), e.ClickedItem);
        }

        private void SetHeaderStyle(Grid header)
        {
            var selectedFeed = (KeyValuePair<FeedCategory, IncrementalLoadingFeedCollection>)pivot.SelectedItem;
            var headerFeed = (KeyValuePair<FeedCategory, IncrementalLoadingFeedCollection>)header.DataContext;

            var textBlock = header.GetDescendantsOfType<TextBlock>().Single();
            var border = header.GetDescendantsOfType<Border>().Single();

            if (selectedFeed.Key == headerFeed.Key)
            {
                // 当前 Pivot 页。

                ResourceDictionary resource;
                if (Settings.Instance.ThemeMode == ThemeMode.Day)
                {
                    resource = (ResourceDictionary)Application.Current.Resources.FindValue("Light");
                }
                else
                {
                    resource = (ResourceDictionary)Application.Current.Resources.FindValue("Dark");
                }

                var u148ThemeBrush = (SolidColorBrush)resource["U148ThemeBrush"];
                textBlock.Foreground = u148ThemeBrush;
                border.Background = u148ThemeBrush;
            }
            else
            {
                ResourceDictionary resource;
                if (Settings.Instance.ThemeMode == ThemeMode.Day)
                {
                    resource = (ResourceDictionary)Application.Current.Resources.FindValue("Light");
                }
                else
                {
                    resource = (ResourceDictionary)Application.Current.Resources.FindValue("Dark");
                }

                textBlock.Foreground = (SolidColorBrush)resource["MainTextBrush"];
                border.Background = new SolidColorBrush(Colors.Transparent);
            }
        }

        private async void ButtonX_Click(object sender, RoutedEventArgs e)
        {
            EmailMessage email = new EmailMessage();
            email.To.Add(new EmailRecipient("h82258652@hotmail.com"));
            email.Subject = "U148有意思吧beta反馈";
            await EmailManager.ShowComposeNewEmailAsync(email);
        }

        private void Ellipse_PointerReleased(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(UserPage));
        }
    }
}