using Newtonsoft.Json;
using SoftwareKobo.U148.Models;
using SoftwareKobo.U148.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

namespace SoftwareKobo.U148.Services
{
    public class CommentService : ICommentService
    {
        private const string GET_COMMENT_LINK_TEMPLATE = @"http://api.u148.net/json/get_comment/{0}/{1}";

        private const string SEND_COMMENT_LINK_TEMPLATE = @"http://api.u148.net/json/comment";

        public async Task<CommentDocument> GetCommentAsync(Feed feed, int page = 1)
        {
            string url = string.Format(CultureInfo.InvariantCulture, GET_COMMENT_LINK_TEMPLATE, feed.Id, page);
            Uri uri = new Uri(url, UriKind.Absolute);
            string json;
            using (HttpClient client = new HttpClient())
            {
                json = await client.GetStringAsync(uri);
            }
            CommentDocument document = JsonConvert.DeserializeObject<CommentDocument>(json);
            return document;
        }

        public async Task<SendCommentResult> SendCommentAsync(Feed feed, UserInfo user, string content, Device device = Device.Android, Comment reviewComment = null)
        {
            Uri uri = new Uri(SEND_COMMENT_LINK_TEMPLATE, UriKind.Absolute);

            Dictionary<string, string> postData = new Dictionary<string, string>();
            postData.Add("id", feed.Id.ToString());
            postData.Add("token", user.Token);
            switch (device)
            {
                case Device.IPhone:
                    postData.Add("client", "iphone");
                    break;

                default:
                    postData.Add("client", "android");
                    break;
            }
            postData.Add("content", content);
            if (reviewComment != null)
            {
                postData.Add("review_id", reviewComment.Id.ToString());
            }

            string json;
            using (HttpClient client = new HttpClient())
            {
                using (HttpContent httpContent = new FormUrlEncodedContent(postData))
                {
                    json = await (await client.PostAsync(uri, httpContent)).Content.ReadAsStringAsync();
                }
            }

            SendCommentResult result = JsonConvert.DeserializeObject<SendCommentResult>(json);
            return result;
        }
    }
}