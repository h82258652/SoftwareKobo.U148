﻿using GalaSoft.MvvmLight;
using SoftwareKobo.U148.DataModels;
using SoftwareKobo.U148.Datas;
using SoftwareKobo.U148.Models;
using SoftwareKobo.U148.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SoftwareKobo.U148.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IFeedService _feedService;

        private readonly Dictionary<FeedCategory, IncrementalLoadingFeedCollection> _categories = new Dictionary<FeedCategory, IncrementalLoadingFeedCollection>();

        public MainPageViewModel(IFeedService feedService)
        {
            _feedService = feedService;

            User = new User();

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

        private User _user;

        public User User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                RaisePropertyChanged(nameof(User));
            }
        }
    }
}