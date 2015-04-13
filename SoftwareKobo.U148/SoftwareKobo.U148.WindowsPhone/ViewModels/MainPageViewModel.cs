using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using SoftwareKobo.U148.Models;
using SoftwareKobo.U148.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;

namespace SoftwareKobo.U148.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IFeedService _feedService;

        private readonly Dictionary<FeedCategory, IncrementalLoadingFeedCollection> _categories = new Dictionary<FeedCategory, IncrementalLoadingFeedCollection>();

        public MainPageViewModel(IFeedService feedService)
        {
            _feedService = feedService;

            var categoryEnums = Enum.GetValues(typeof(FeedCategory)).Cast<FeedCategory>();
            foreach (var categoryEnum in categoryEnums)
            {
                _categories.Add(categoryEnum, new IncrementalLoadingFeedCollection(_feedService, categoryEnum));
            }
        }

        public Dictionary<FeedCategory, IncrementalLoadingFeedCollection> Categories
        {
            get
            {
                return _categories;
            }
        }

        private RelayCommand<ItemClickEventArgs> _feedClickCommand;

        public RelayCommand<ItemClickEventArgs> FeedClickCommand
        {
            get
            {
                _feedClickCommand = _feedClickCommand ?? new RelayCommand<ItemClickEventArgs>(args => Messenger.Default.Send(args));
                return _feedClickCommand;
            }
        }
    }
}