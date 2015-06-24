using SoftwareKobo.U148.DataModels.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace SoftwareKobo.U148.DataModels
{
    public abstract class IncrementalLoadingCollectionBase<T> : ObservableCollection<T>, ISupportIncrementalLoading, IRefreshable
    {
        private bool _isLoading;

        protected bool _hadLoadOnce;

        private DateTime _lastLoaded;

        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            protected set
            {
                _isLoading = value;
                this.OnPropertyChanged(new PropertyChangedEventArgs("IsLoading"));
            }
        }

        public virtual bool HasMoreItems
        {
            get
            {
                return true;
            }
        }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            if (IsLoading)
            {
                return AsyncInfo.Run(c =>
                {
                    return Task.Run(() =>
                    {
                        return new LoadMoreItemsResult()
                        {
                            Count = 0
                        };
                    });
                });
            }

            IsLoading = true;

            return AsyncInfo.Run(c =>
            {
                try
                {
                    return LoadMoreItemsAsync(c, count);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);

                    return Task.Run(() =>
                    {
                        return new LoadMoreItemsResult() { Count = 0 };
                    });
                }
            });
        }

        protected abstract Task<LoadMoreItemsResult> LoadMoreItemsAsync(CancellationToken c, uint count);

        public virtual async Task Refresh()
        {
            _hadLoadOnce = false;
            ClearItems();

            await LoadMoreItemsAsync(1);
        }

        public DateTime LastLoaded
        {
            get
            {
                return _lastLoaded;
            }
            protected set
            {
                _lastLoaded = value;
                this.OnPropertyChanged(new PropertyChangedEventArgs("LastLoaded"));
            }
        }
    }
}