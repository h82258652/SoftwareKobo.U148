using SoftwareKobo.U148.Controls.Extensions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Phone.UI.Input;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using WinRTXamlToolkit.AwaitableUI;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

namespace SoftwareKobo.U148.Controls
{
    [ContentProperty(Name = nameof(PrimaryCommands))]
    public sealed class ApplicationBar : Control
    {
        public static readonly DependencyProperty IsLeftSwitchEnabledProperty = DependencyProperty.Register(nameof(IsLeftSwitchEnabled), typeof(bool), typeof(ApplicationBar), new PropertyMetadata(true, IsLeftSwitchEnabledChanged));

        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register(nameof(IsOpen), typeof(bool), typeof(ApplicationBar), new PropertyMetadata(false, IsOpenChanged));

        public static readonly DependencyProperty PrimaryCommandsProperty = DependencyProperty.Register(nameof(PrimaryCommands), typeof(ObservableCollection<UIElement>), typeof(ApplicationBar), new PropertyMetadata(null));

        public static readonly DependencyProperty SecondaryCommandsProperty = DependencyProperty.Register(nameof(SecondaryCommands), typeof(ObservableCollection<UIElement>), typeof(ApplicationBar), new PropertyMetadata(null));

        private static readonly DependencyProperty PrimaryBackgroundProperty = DependencyProperty.Register(nameof(PrimaryBackground), typeof(Brush), typeof(ApplicationBar), new PropertyMetadata(null));

        private static readonly DependencyProperty SecondaryBackgroundProperty = DependencyProperty.Register(nameof(SecondaryBackground), typeof(Brush), typeof(ApplicationBar), new PropertyMetadata(null));

        private FrameworkElement PART_leftSwitch;

        private FrameworkElement PART_rightSwitch;

        private FrameworkElement PART_secondary;

        public ApplicationBar()
        {
            this.DefaultStyleKey = typeof(ApplicationBar);

            this.PrimaryCommands = new ObservableCollection<UIElement>();
            this.SecondaryCommands = new ObservableCollection<UIElement>();

            if (DesignMode.DesignModeEnabled == false)
            {
                this.Loaded += this.ApplicationBar_Loaded;
                this.Unloaded += this.ApplicationBar_Unloaded;
            }
        }

        public event EventHandler<bool> Closed;

        public event EventHandler<bool> Opened;

        public bool IsLeftSwitchEnabled
        {
            get
            {
                return (bool)GetValue(IsLeftSwitchEnabledProperty);
            }
            set
            {
                SetValue(IsLeftSwitchEnabledProperty, value);
            }
        }

        public bool IsOpen
        {
            get
            {
                return (bool)GetValue(IsOpenProperty);
            }
            set
            {
                SetValue(IsOpenProperty, value);
            }
        }

        public Brush PrimaryBackground
        {
            get
            {
                return (Brush)GetValue(PrimaryBackgroundProperty);
            }
            set
            {
                SetValue(PrimaryBackgroundProperty, value);
            }
        }

        public ObservableCollection<UIElement> PrimaryCommands
        {
            get
            {
                return (ObservableCollection<UIElement>)GetValue(PrimaryCommandsProperty);
            }
            private set
            {
                SetValue(PrimaryCommandsProperty, value);
            }
        }

        public Brush SecondaryBackground
        {
            get
            {
                return (Brush)GetValue(SecondaryBackgroundProperty);
            }
            set
            {
                SetValue(SecondaryBackgroundProperty, value);
            }
        }

        public ObservableCollection<UIElement> SecondaryCommands
        {
            get
            {
                return (ObservableCollection<UIElement>)GetValue(SecondaryCommandsProperty);
            }
            private set
            {
                SetValue(SecondaryCommandsProperty, value);
            }
        }

        protected override void OnApplyTemplate()
        {
            PART_leftSwitch = (FrameworkElement)GetTemplateChild(nameof(PART_leftSwitch));
            // 初始化左侧开关。
            PART_leftSwitch.Visibility = IsLeftSwitchEnabled ? Visibility.Visible : Visibility.Collapsed;
            PART_leftSwitch.PointerReleased += Switch_PointerReleased;

            PART_rightSwitch = (FrameworkElement)GetTemplateChild(nameof(PART_rightSwitch));
            PART_rightSwitch.PointerReleased += Switch_PointerReleased;

            PART_rightSwicthSymbol = (FrameworkElement)GetTemplateChild(nameof(PART_rightSwicthSymbol));

            PART_secondary = (FrameworkElement)GetTemplateChild(nameof(PART_secondary));
        }

        private void Switch_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            IsOpen = !IsOpen;
        }

        private static void IsLeftSwitchEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ApplicationBar applicationBar = (ApplicationBar)d;
            if (applicationBar.PART_leftSwitch != null)
            {
                bool value = (bool)e.NewValue;
                applicationBar.PART_leftSwitch.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private static void IsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == e.OldValue)
            {
                return;
            }

            ApplicationBar applicationBar = (ApplicationBar)d;
            bool isOpen = (bool)e.NewValue;
            if (isOpen)
            {
                applicationBar.Open();
            }
            else
            {
                applicationBar.Close();
            }
        }

        private void ApplicationBar_Loaded(object sender, RoutedEventArgs e)
        {
            var coreWindow = Window.Current.CoreWindow;
            coreWindow.PointerPressed += CoreWindow_PointerPressed;

            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        private async void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            if (this.IsOpen)
            {
                e.Handled = true;
                // 防止与双击后退退出冲突。
                await Task.Delay(1);
                this.IsOpen = false;
            }
        }

        private void ApplicationBar_Unloaded(object sender, RoutedEventArgs e)
        {
            var coreWindow = Window.Current.CoreWindow;
            coreWindow.PointerPressed -= CoreWindow_PointerPressed;
        }

        private FrameworkElement PART_rightSwicthSymbol;

        private void Close()
        {
            Storyboard storyboard = new Storyboard();

            double duration = 0.8d * PART_secondary.ActualHeight / PART_secondary.MaxHeight;

            #region 列表关闭动画

            DoubleAnimation listAnimation = new DoubleAnimation();
            listAnimation.EnableDependentAnimation = true;
            listAnimation.From = PART_secondary.ActualHeight;
            listAnimation.To = 0;
            listAnimation.EasingFunction = new ExponentialEase() { Exponent = 10 };
            listAnimation.Duration = new Duration(TimeSpan.FromSeconds(duration));
            Storyboard.SetTarget(listAnimation, PART_secondary);
            Storyboard.SetTargetProperty(listAnimation, "Height");
            storyboard.Children.Add(listAnimation);

            storyboard.Completed += (sender, e) =>
            {
                PART_secondary.Visibility = Visibility.Collapsed;
                PART_secondary.Height = double.NaN;
            };

            #endregion 列表关闭动画

            PlayRightSwitchAnimation(duration);

            storyboard.Begin();

            if (Closed != null)
            {
                Closed(this, IsOpen);
            }
        }

        private void CoreWindow_PointerPressed(CoreWindow sender, PointerEventArgs args)
        {
            if (this.IsPointOn(args.CurrentPoint.Position) == false)
            {
                // 点击 ApplicationBar 以外的区域。
                IsOpen = false;
            }
        }

        private async void Open()
        {
            #region 获取所需高度

            PART_secondary.Visibility = Visibility.Visible;
            await PART_secondary.WaitForLayoutUpdateAsync();
            double partSecondaryAvailableHeight = PART_secondary.ActualHeight;

            #endregion 获取所需高度

            // 动画持续时间。
            double duration = 1.0d * partSecondaryAvailableHeight / PART_secondary.MaxHeight;

            Storyboard storyboard = new Storyboard();

            #region 列表展开动画

            DoubleAnimation listAnimation = new DoubleAnimation();
            listAnimation.EnableDependentAnimation = true;
            listAnimation.From = 0;
            listAnimation.To = partSecondaryAvailableHeight;
            listAnimation.EasingFunction = new ExponentialEase()
            {
                Exponent = 10
            };
            listAnimation.Duration = new Duration(TimeSpan.FromSeconds(duration));
            Storyboard.SetTarget(listAnimation, PART_secondary);
            Storyboard.SetTargetProperty(listAnimation, "Height");
            storyboard.Children.Add(listAnimation);

            #endregion 列表展开动画

            #region 列表元素动画

            double deltaY = 10;
            foreach (var command in SecondaryCommands)
            {
                var translate = new TranslateTransform();
                command.RenderTransform = translate;

                DoubleAnimation commandAnimation = new DoubleAnimation();
                commandAnimation.From = deltaY;
                commandAnimation.To = 0;
                commandAnimation.Duration = new Duration(TimeSpan.FromSeconds(duration / 2));
                Storyboard.SetTarget(commandAnimation, translate);
                Storyboard.SetTargetProperty(commandAnimation, "Y");
                storyboard.Children.Add(commandAnimation);

                deltaY += 10;
            }

            #endregion 列表元素动画

            storyboard.Begin();

            #region 右侧开关动画

            PlayRightSwitchAnimation(duration);

            #endregion 右侧开关动画

            if (Opened != null)
            {
                Opened(this, IsOpen);
            }
        }

        private void PlayRightSwitchAnimation(double duration)
        {
            Storyboard storyboard = new Storyboard();

            var rotate = new RotateTransform();
            PART_rightSwicthSymbol.RenderTransform = rotate;

            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0;
            animation.To = 180;
            animation.Duration = new Duration(TimeSpan.FromSeconds(duration));
            Storyboard.SetTarget(animation, rotate);
            Storyboard.SetTargetProperty(animation, "Angle");
            storyboard.Children.Add(animation);

            storyboard.Begin();
        }
    }
}