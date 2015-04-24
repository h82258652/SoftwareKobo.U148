using SoftwareKobo.U148.Models;
using SoftwareKobo.U148.Services.Interfaces;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace SoftwareKobo.U148.Services
{
    public class FavoriteService : IFavoriteService
    {
        private const string GET_FAVORITES_LINK_TEMPLATE = @"http://api.u148.net/json/get_favourite/0/{0}?token={1}";

        private const string ADD_FAVORITE_LINK_TEMPLATE = @"http://api.u148.net/json/favourite?id={0}&token={1}";

        private const string DELETE_FAVORITE_LINK_TEMPLATE = @"";

        public async Task<object> GetFavoritesAsync(UserInfo user, int page = 1)
        {
            string url = string.Format(GET_FAVORITES_LINK_TEMPLATE, page, user.Token);
            Uri uri = new Uri(url, UriKind.Absolute);
            string json;
            using (HttpClient client = new HttpClient())
            {
                json = await client.GetStringAsync(uri);
            }
            Debugger.Break();
            return json;
        }

        public async Task<object> AddFavoriteAsync(Feed feed, UserInfo user)
        {
            string url = string.Format(ADD_FAVORITE_LINK_TEMPLATE, feed.Id, user.Token);
            Uri uri = new Uri(url, UriKind.Absolute);
            string json;
            using (HttpClient client = new HttpClient())
            {
                json = await client.GetStringAsync(uri);
            }
            Debugger.Break();
            return json;
        }

        public async Task<object> DeleteFavoriteAsync(Feed feed, UserInfo user)
        {
            string url = string.Format(DELETE_FAVORITE_LINK_TEMPLATE, feed.Id, user.Token);
            Uri uri = new Uri(url, UriKind.Absolute);
            string json;
            using (HttpClient client = new HttpClient())
            {
                json = await client.GetStringAsync(uri);
            }
            Debugger.Break();
            return json;
        }
    }
}