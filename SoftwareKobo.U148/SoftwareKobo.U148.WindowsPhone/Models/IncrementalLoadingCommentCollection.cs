using SoftwareKobo.U148.Services.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace SoftwareKobo.U148.Models
{
    public class IncrementalLoadingCommentCollection : ObservableCollection<Comment>, ISupportIncrementalLoading, INotifyPropertyChanged
    {
        private readonly ICommentService _commentService;

        private readonly Feed _feed;

        private int _currentPage;

        private int _pageCount;

        private bool _isLoading;

        private bool _hasLoadOnce;

        public IncrementalLoadingCommentCollection(ICommentService commentService, Feed feed)
        {
            _commentService = commentService;
            _feed = feed;
            _currentPage = 0;
        }

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

        public bool HasMoreItems
        {
            get
            {
                if (_hasLoadOnce == false)
                {
                    // 未加载。
                    return true;
                }
                throw new NotImplementedException();
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

        protected async Task<LoadMoreItemsResult> LoadMoreItemsAsync(CancellationToken c, uint count)
        {
            try
            {
                // 加载下一页。
                CommentDocument document = await _commentService.GetCommentAsync(_feed, _currentPage + 1);
                if (document.Code == 0 && document.Message == "success")
                {
                    // 加载成功。

                    CommentList commentList = document.Data;
                    // 设置该文章评论的最大页数。
                    _pageCount = commentList.PageCount;

                    foreach (Comment comment in commentList)
                    {
                        if (this.Any(temp => temp.Id == comment.Id) == false)
                        {
                            // 不添加已经重复的项。
                            this.Add(comment);
                        }
                    }

                    // 设置当前页数。
                    _currentPage = commentList.Next - 1;

                    return new LoadMoreItemsResult()
                    {
                        Count = (uint)commentList.Count()
                    };
                }
                else
                {
                    // 加载失败。
                    Debug.WriteLine(string.Format(CultureInfo.InvariantCulture, "comment load failed. code: {0}. msg: {1}", document.Code, document.Message));

                    return new LoadMoreItemsResult()
                    {
                        Count = 0
                    };
                }
            }
            catch (HttpRequestException)
            {
                // 网络原因。
                return new LoadMoreItemsResult()
                {
                    Count = 0
                };
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                return new LoadMoreItemsResult()
                {
                    Count = 0
                };
            }
            finally
            {
                _hasLoadOnce = true;
                IsLoading = false;
            }
        }
    }
}