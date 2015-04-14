using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace SoftwareKobo.U148.Controls.Extensions
{
    public static class FrameworkElementExtensions
    {
        /// <summary>
        /// 获取点是否在控件上。
        /// </summary>
        /// <param name="element"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static bool IsPointOn(this FrameworkElement element, Point point)
        {
            GeneralTransform transform = element.TransformToVisual(Window.Current.Content);
            Rect rect = transform.TransformBounds(new Rect(0, 0, element.ActualWidth, element.ActualHeight));
            return rect.Contains(point);
        }
    }
}