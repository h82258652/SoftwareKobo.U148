using GalaSoft.MvvmLight;
using SoftwareKobo.U148.Models;
using SoftwareKobo.U148.Services.Interfaces;

namespace SoftwareKobo.U148.ViewModels
{
    public class DetailPageViewModel : ViewModelBase
    {
        private readonly IFeedService _feedService;

        private FeedDetail _detail;
        private Feed _feed;

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

        public Feed Feed
        {
            get
            {
                return _feed;
            }
            set
            {
                _feed = value;
                RaisePropertyChanged(nameof(Feed));
                LoadDetail();
            }
        }

        public async void LoadDetail()
        {
            var document = await _feedService.GetDetailAsync(Feed);
            Detail = document.Data;
        }
    }
}