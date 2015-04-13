using System;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SoftwareKobo.U148.Extensions
{
    public static class WebViewExtensions
    {
        public static async Task WaitForDOMContentLoaded(this WebView webView)
        {
            var tcs = new TaskCompletionSource<object>();

            TypedEventHandler<WebView, WebViewDOMContentLoadedEventArgs> nceh = null;

            nceh = (s, e) =>
            {
                webView.DOMContentLoaded -= nceh;
                tcs.SetResult(null);
            };

            webView.DOMContentLoaded += nceh;

            await tcs.Task;
        }




        public static readonly DependencyProperty HtmlProperty = DependencyProperty.RegisterAttached("Html", typeof(string), typeof(WebViewExtensions), new PropertyMetadata(string.Empty, HtmlChanged));

        private static void HtmlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WebView webView = (WebView)d;
            string html = (string)e.NewValue;
            if (html != null)
            {
                webView.NavigateToString(html);
            }
        }

        public static void SetHtml(WebView webView, string html)
        {
            webView.SetValue(HtmlProperty, html);
        }

        public static string GetHtml(WebView webView)
        {
            return (string)webView.GetValue(HtmlProperty);
        }

        public static readonly DependencyProperty ContentProperty = DependencyProperty.RegisterAttached("Content", typeof(string), typeof(WebViewExtensions), new PropertyMetadata(string.Empty, ContentChanged));

        private static async void ContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WebView webView = (WebView)d;
            string content = (string)e.NewValue;
            if (content != null)
            {
                try
                {
                    await webView.InvokeScriptAsync("setContent", new string[] { content });
                }
                catch
                {

                }
            }
        }

        public static void SetContent(WebView webView, string content)
        {
            webView.SetValue(ContentProperty, content);
        }

        public static string GetContent(WebView webView)
        {
            return (string)webView.GetValue(ContentProperty);
        }
    }
}