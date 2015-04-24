using SoftwareKobo.U148.DataModels.Interfaces;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SoftwareKobo.U148.Controls
{
    public sealed partial class PullToRefreshListView : UserControl
    {
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(nameof(ItemsSource), typeof(object), typeof(PullToRefreshListView), new PropertyMetadata(null, ItemsSourceChanged));

        public static readonly DependencyProperty ItemTemplateProperty = ListView.ItemTemplateProperty;

        private UISettings _uiSettings;

        public PullToRefreshListView()
        {
            this.InitializeComponent();

            if (DesignMode.DesignModeEnabled == false)
            {
                this.Loaded += PullToRefreshListView_Loaded;

                this.Unloaded += PullToRefreshListView_Unloaded;
            }
        }

        public event ItemClickEventHandler ItemClick
        {
            add
            {
                lvw.ItemClick += value;
            }
            remove
            {
                lvw.ItemClick -= value;
            }
        }

        public object ItemsSource
        {
            get
            {
                return GetValue(ItemsSourceProperty);
            }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }

        public DataTemplate ItemTemplate
        {
            get
            {
                return lvw.ItemTemplate;
            }
            set
            {
                lvw.ItemTemplate = value;
            }
        }

        private static void ItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PullToRefreshListView listView = (PullToRefreshListView)d;
            listView.lvw.ItemsSource = e.NewValue;
        }

        private void PullToRefreshListView_Loaded(object sender, RoutedEventArgs e)
        {
            _uiSettings = new UISettings();
            UpdateSymbolScale(_uiSettings.TextScaleFactor);
            _uiSettings.TextScaleFactorChanged += PullToRefreshListView_TextScaleFactorChanged;
        }

        private void PullToRefreshListView_TextScaleFactorChanged(UISettings sender, object args)
        {
            UpdateSymbolScale(sender.TextScaleFactor);
        }

        private void PullToRefreshListView_Unloaded(object sender, RoutedEventArgs e)
        {
            _uiSettings.TextScaleFactorChanged -= PullToRefreshListView_TextScaleFactorChanged;
        }

        private void PullToRefreshPanel_Pulling(object sender, PullingEventArgs e)
        {
            if (e.State == PullingState.Pull)
            {
                if (e.Offset > ctlSymbol.ActualHeight)
                {
                    var pullContentHeight = grdPullContent.ActualHeight;
                    symbolRotate.Angle = -90 * (e.Offset - ctlSymbol.ActualHeight) / (pullContentHeight - ctlSymbol.ActualHeight);
                }
                else
                {
                    symbolRotate.Angle = 0;
                }

                txtPullToRefreshNotice.Text = "下拉刷新";
            }
            else if (e.State == PullingState.PrepareRefresh)
            {
                symbolRotate.Angle = -90;

                txtPullToRefreshNotice.Text = "松开刷新";
            }
            else if (e.State == PullingState.Refreshing)
            {
                txtPullToRefreshNotice.Text = "正在刷新";
            }
        }

        private async Task PullToRefreshPanel_Refreshing(object sender, EventArgs e)
        {
            var refreshableCollection = lvw.ItemsSource as IRefreshable;
            if (refreshableCollection != null)
            {
                await refreshableCollection.Refresh();
            }
        }

        private void UpdateSymbolScale(double scale)
        {
            symbolScale.ScaleX = scale;
            symbolScale.ScaleY = scale;
        }
    }
}