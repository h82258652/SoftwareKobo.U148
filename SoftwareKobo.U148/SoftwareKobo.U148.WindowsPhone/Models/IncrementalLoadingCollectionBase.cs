using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace SoftwareKobo.U148.Models
{
    public abstract class IncrementalLoadingCollectionBase<T> : ObservableCollection<T>, ISupportIncrementalLoading
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
                this.OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsLoading)));
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
                throw new InvalidOperationException("Only one operation in flight at a time");
            }

            IsLoading = true;

            return AsyncInfo.Run(c => LoadMoreItemsAsync(c, count));
        }

        protected abstract Task<LoadMoreItemsResult> LoadMoreItemsAsync(CancellationToken c, uint count);
        
        public virtual async Task Refresh()
        {
            _hadLoadOnce = false;
            ClearItems();

            if (IsLoading == false)
            {
                await Task.Factory.StartNew(() =>
                {
                    while (IsLoading == false)
                    {
                    }
                });
            }
            await Task.Factory.StartNew(() =>
            {
                while (IsLoading)
                {
                }
            });
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
                this.OnPropertyChanged(new PropertyChangedEventArgs(nameof(LastLoaded)));
            }
        }
    }
}