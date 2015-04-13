using Newtonsoft.Json;
using SoftwareKobo.U148.Models;
using SoftwareKobo.U148.Services.Interfaces;
using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

namespace SoftwareKobo.U148.Services
{
    public class FeedService : IFeedService
    {
        private const string CATEGORY_LINK_TEMPLATE = @"http://api.u148.net/json/{0}/{1}";
        private const string DETAIL_LINK_TEMPLATE = @"http://api.u148.net/json/article/{0}";

        public async Task<FeedListDocument> GetCategoryAsync(FeedCategory category, int page = 1)
        {
            string url = string.Format(CultureInfo.InvariantCulture, CATEGORY_LINK_TEMPLATE, (int)category, page);
            Uri uri = new Uri(url, UriKind.Absolute);
            string json;
            using (HttpClient client = new HttpClient())
            {
                json = await client.GetStringAsync(uri);
            }
            FeedListDocument document = JsonConvert.DeserializeObject<FeedListDocument>(json);
            return document;
        }

        public async Task<FeedDetailDocument> GetDetailAsync(Feed feed)
        {
            string url = string.Format(CultureInfo.InvariantCulture, DETAIL_LINK_TEMPLATE, feed.Id);
            Uri uri = new Uri(url, UriKind.Absolute);
            string json;
            using (HttpClient client = new HttpClient())
            {
                json = await client.GetStringAsync(uri);
            }
            FeedDetailDocument detail = JsonConvert.DeserializeObject<FeedDetailDocument>(json);
            return detail;
        }
    }
}