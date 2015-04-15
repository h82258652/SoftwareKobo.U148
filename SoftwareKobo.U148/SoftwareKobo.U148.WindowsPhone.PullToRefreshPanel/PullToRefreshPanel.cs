using System;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

namespace SoftwareKobo.U148.Controls
{
    public class PullToRefreshPanel : ContentControl
    {
        public static readonly DependencyProperty PullContentProperty = DependencyProperty.Register(nameof(PullContent), typeof(object), typeof(PullToRefreshPanel), new PropertyMetadata(null));

        private FrameworkElement PART_contentGrid;

        private FrameworkElement PART_pullGrid;

        private ScrollViewer PART_scrollViewer;

        public PullToRefreshPanel()
        {
            this.DefaultStyleKey = typeof(PullToRefreshPanel);

            if (DesignMode.DesignModeEnabled == false)
            {
                this.Loaded += PullToRefreshPanel_Loaded;
                this.SizeChanged += PullToRefreshPanel_SizeChanged;
            }
        }

        public event EventHandler<PullingEventArgs> Pulling;

        public event EventHandler PullToRefresh;

        public event RefreshingEventHandler Refreshing;

        public object PullContent
        {
            get
            {
                return GetValue(PullContentProperty);
            }
            set
            {
                SetValue(PullContentProperty, value);
            }
        }

        protected override void OnApplyTemplate()
        {
            PART_scrollViewer = (ScrollViewer)GetTemplateChild(nameof(PART_scrollViewer));
            PART_scrollViewer.ViewChanged += PART_scrollViewer_ViewChanged;

            PART_pullGrid = (FrameworkElement)GetTemplateChild(nameof(PART_pullGrid));

            PART_contentGrid = (FrameworkElement)GetTemplateChild(nameof(PART_contentGrid));
        }

        private async void PART_scrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            UpdateState(true);

            if (e.IsIntermediate)
            {
                // 指针未松开。
                return;
            }

            if (PART_scrollViewer.VerticalOffset <= 0)
            {
                VisualStateManager.GoToState(this, "Refreshing", true);
                if (Pulling != null)
                {
                    Pulling(this, new PullingEventArgs(PART_pullGrid.ActualHeight, PullingState.Refreshing));
                }

                // 触发请求刷新事件。
                if (PullToRefresh != null)
                {
                    PullToRefresh(this, EventArgs.Empty);
                }

                // 执行异步加载事件。
                if (Refreshing != null)
                {
                    await Refreshing(this, EventArgs.Empty);
                }
            }

            // 将下拉部分弹回默认隐藏显示。
            PART_scrollViewer.ChangeView(null, PART_pullGrid.ActualHeight, null);
        }

        private void PullToRefreshPanel_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateContentGrid();
        }

        private void PullToRefreshPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateContentGrid();
        }

        private void UpdateContentGrid()
        {
            if (PART_contentGrid != null)
            {
                PART_scrollViewer.ChangeView(null, PART_pullGrid.ActualHeight, null);

                PART_contentGrid.Width = this.ActualWidth;
                PART_contentGrid.Height = this.ActualHeight;
            }
        }

        private void UpdateState(bool useTransitions)
        {
            if (PART_scrollViewer.VerticalOffset <= 0.0d)
            {
                VisualStateManager.GoToState(this, "Refresh", useTransitions);
                if (Pulling != null)
                {
                    Pulling(this, new PullingEventArgs(PART_pullGrid.ActualHeight, PullingState.PrepareRefresh));
                }
            }
            else
            {
                VisualStateManager.GoToState(this, "Pull", useTransitions);
                if (Pulling != null)
                {
                    Pulling(this, new PullingEventArgs(PART_pullGrid.ActualHeight - PART_scrollViewer.VerticalOffset, PullingState.Pull));
                }
            }
        }
    }
}