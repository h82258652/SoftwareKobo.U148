using Newtonsoft.Json;
using SoftwareKobo.U148.Models;
using SoftwareKobo.U148.Services.Interfaces;
using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

namespace SoftwareKobo.U148.Services
{
    public class CommentService : ICommentService
    {
        private const string COMMENT_LINK_TEMPLATE = @"http://api.u148.net/json/get_comment/{0}/{1}";

        public async Task<CommentDocument> GetCommentAsync(Feed feed, int page = 1)
        {
            string url = string.Format(CultureInfo.InvariantCulture,COMMENT_LINK_TEMPLATE,feed.Id,page);
            Uri uri = new Uri(url, UriKind.Absolute);
            string json;
            using (HttpClient client=new HttpClient())
            {
                json = await client.GetStringAsync(uri);
            }
            CommentDocument document = JsonConvert.DeserializeObject<CommentDocument>(json);
            return document;
        }
    }
}