using SoftwareKobo.U148.Models;
using SoftwareKobo.U148.Services.Interfaces;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace SoftwareKobo.U148.DataModels
{
    public class IncrementalLoadingCommentCollection : IncrementalLoadingCollectionBase<Comment>
    {
        private readonly ICommentService _commentService;

        private readonly Feed _feed;

        private int _currentPage;

        private int _pageCount;

        public IncrementalLoadingCommentCollection(ICommentService commentService, Feed feed)
        {
            _commentService = commentService;
            _feed = feed;
            _currentPage = 0;
        }

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

        protected override async Task<LoadMoreItemsResult> LoadMoreItemsAsync(CancellationToken c, uint count)
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

                    _hadLoadOnce = true;

                    LastLoaded = DateTime.Now;

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
            finally
            {
                IsLoading = false;
            }
        }
    }
}