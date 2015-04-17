using GalaSoft.MvvmLight;
using SoftwareKobo.U148.DataModels;
using SoftwareKobo.U148.Models;
using SoftwareKobo.U148.Services.Interfaces;

namespace SoftwareKobo.U148.ViewModels
{
    public class CommentPageViewModel : ViewModelBase
    {
        private readonly ICommentService _commentService;

        private IncrementalLoadingCommentCollection _comments;

        private Feed _feed;

        public CommentPageViewModel(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public void SetFeed(Feed feed)
        {
            _feed = feed;
            _comments = new IncrementalLoadingCommentCollection(_commentService, feed);
        }

        public IncrementalLoadingCommentCollection Comments
        {
            get
            {
                return _comments;
            }
        }
    }
}