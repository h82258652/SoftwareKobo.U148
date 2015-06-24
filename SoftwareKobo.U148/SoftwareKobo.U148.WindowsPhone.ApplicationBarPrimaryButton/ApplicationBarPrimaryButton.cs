using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

namespace SoftwareKobo.U148.Controls
{
    public class ApplicationBarPrimaryButton : Button
    {
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(object), typeof(ApplicationBarPrimaryButton), new PropertyMetadata(null));

        public static readonly DependencyProperty IsCompactProperty = DependencyProperty.Register("IsCompact", typeof(bool), typeof(ApplicationBarPrimaryButton), new PropertyMetadata(false, IsCompactChanged));

        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(object), typeof(ApplicationBarPrimaryButton), new PropertyMetadata(null));

        public ApplicationBarPrimaryButton()
        {
            this.DefaultStyleKey = typeof(ApplicationBarPrimaryButton);
        }

        public object Icon
        {
            get
            {
                return GetValue(IconProperty);
            }
            set
            {
                SetValue(IconProperty, value);
            }
        }

        public bool IsCompact
        {
            get
            {
                return (bool)GetValue(IsCompactProperty);
            }
            set
            {
                SetValue(IsCompactProperty, value);
            }
        }

        public object Label
        {
            get
            {
                return GetValue(LabelProperty);
            }
            set
            {
                SetValue(LabelProperty, value);
            }
        }

        private static void IsCompactChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ApplicationBarPrimaryButton button = (ApplicationBarPrimaryButton)d;
            bool value = (bool)e.NewValue;
            if (value)
            {
                VisualStateManager.GoToState(button, "Compact", true);
            }
            else
            {
                VisualStateManager.GoToState(button, "FullSize", true);
            }
        }
    }
}