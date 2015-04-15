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

        public string Content
        {
            get
            {
                if (_detail == null)
                {
                    return null;
                }
                else
                {
                    return _detail.Content;
                }
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
                RaisePropertyChanged(nameof(Detail));
                RaisePropertyChanged(nameof(Content));
            }
        }

        public void SetFeed(Feed feed)
        {
            _feed = feed;
            LoadDetail();
        }

        public RelayCommand GetCommentCommand
        {
            get
            {
                _getCommentCommand = _getCommentCommand ?? new RelayCommand(() =>
                    {
                        Messenger.Default.Send(_feed);
                    });
                return _getCommentCommand;
            }
        }

        public async void LoadDetail()
        {
            while (true)
            {
                try
                {
                    var document = await _feedService.GetDetailAsync(_feed);
                    Detail = document.Data;
                    break;
                }
                catch
                {
                }
            }
        }
    }
}