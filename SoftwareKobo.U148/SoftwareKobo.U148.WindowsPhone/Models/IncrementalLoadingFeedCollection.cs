using SoftwareKobo.U148.Services.Interfaces;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace SoftwareKobo.U148.Models
{
    public class IncrementalLoadingFeedCollection : IncrementalLoadingCollectionBase<Feed>
    {
        private IFeedService _feedService;

        private FeedCategory _category;

        private int _currentPage;

        private int _pageCount;

        public override Task Refresh()
        {
            _currentPage = 0;
            _pageCount = 0;

            return base.Refresh();
        }

        public override bool HasMoreItems
        {
            get
            {
                if (_hadLoadOnce == false)
                {
                    // 未加载。
                    return true;
                }

                return _currentPage < _pageCount;
            }
        }

        public IncrementalLoadingFeedCollection(IFeedService feedService, FeedCategory category)
        {
            _feedService = feedService;
            _category = category;
            _currentPage = 0;
        }

        protected override async Task<LoadMoreItemsResult> LoadMoreItemsAsync(CancellationToken c, uint count)
        {
            try
            {
                // 加载下一页。
                FeedListDocument document = await _feedService.GetCategoryAsync(_category, _currentPage + 1);
                if (document.Code == 0 && document.Message == "success")
                {
                    // 加载成功。

                    FeedList feedList = document.Data;
                    // 设置该分类的最大页数。
                    _pageCount = feedList.PageCount;

                    foreach (Feed feed in feedList)
                    {
                        if (this.Any(temp => temp.Id == feed.Id) == false)
                        {
                            // 不添加已经重复的项。
                            this.Add(feed);
                        }
                    }

                    // 设置当前页数。
                    _currentPage = feedList.Next - 1;

                    _hadLoadOnce = true;

                    LastLoaded = DateTime.Now;

                    return new LoadMoreItemsResult()
                    {
                        Count = (uint)feedList.Count()
                    };
                }
                else
                {
                    // 加载失败。
                    Debug.WriteLine(string.Format(CultureInfo.InvariantCulture, "category load failed. code: {0}. msg: {1}", document.Code, document.Message));

                    return new LoadMoreItemsResult()
                    {
                        Count = 0
                    };
                }
            }
            catch (HttpRequestException)
            {
                // 网络原因，不处理。让 UI 继续读条。
                return new LoadMoreItemsResult() { Count = 0 };
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                return new LoadMoreItemsResult() { Count = 0 };
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}