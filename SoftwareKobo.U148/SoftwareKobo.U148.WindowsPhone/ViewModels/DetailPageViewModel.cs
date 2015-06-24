using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using SoftwareKobo.U148.Models;
using SoftwareKobo.U148.Services.Interfaces;

namespace SoftwareKobo.U148.ViewModels
{
    public class DetailPageViewModel : ViewModelBase
    {
        private readonly IFeedService _feedService;

        private FeedDetail _detail;

        private Feed _feed;

        private RelayCommand _getCommentCommand;

        public DetailPageViewModel(IFeedService feedService)
        {
            _feedService = feedService;
        }

        public Feed Feed
        {
            get
            {
                return _feed;
            }
            private set
            {
                _feed = value;
                RaisePropertyChanged("Feed");
            }
        }

        public FeedDetail Detail
        {
            get
            {
                return _detail;
            }
            private set
            {
                _detail = value;
                RaisePropertyChanged("Detail");
            }
        }

        public void SetFeed(Feed feed)
        {
            Feed = feed;
            LoadDetail();
        }

        public RelayCommand GetCommentCommand
        {
            get
            {
                _getCommentCommand = _getCommentCommand ?? new RelayCommand(() =>
                    {
                        Messenger.Default.Send<Feed>(_feed);
                    });
                return _getCommentCommand;
            }
        }

        public async void LoadDetail()
        {
            while (true)
            {
                // 直到加载完成。
                try
                {
                    var document = await _feedService.GetDetailAsync(_feed);
                    Detail = document.Data;
                    Messenger.Default.Send<string>(Detail.Content);
                    break;
                }
                catch
                {
                }
            }
        }
    }
}