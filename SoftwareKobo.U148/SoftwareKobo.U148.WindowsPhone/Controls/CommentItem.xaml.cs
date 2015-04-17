using SoftwareKobo.U148.Models;
using Windows.Data.Html;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SoftwareKobo.U148.Controls
{
    public sealed partial class CommentItem : UserControl
    {
        public Comment Comment
        {
            get
            {
                return (Comment)this.DataContext;
            }
        }

        public CommentItem()
        {
            this.InitializeComponent();

            this.DataContextChanged += CommentItem_DataContextChanged;
        }

        private void CommentItem_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            var comment = args.NewValue as Comment;
            if (comment != null)
            {
                txtContent.Text = HtmlUtilities.ConvertToText(comment.Contents);
            }
        }
    }
}