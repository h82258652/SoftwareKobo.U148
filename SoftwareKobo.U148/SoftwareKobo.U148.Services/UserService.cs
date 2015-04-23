using Newtonsoft.Json;
using SoftwareKobo.U148.Models;
using SoftwareKobo.U148.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SoftwareKobo.U148.Services
{
    public class UserService : IUserService
    {
        private const string LOGIN_LINK_TEMPLATE = @"http://api.u148.net/json/login";

        public async Task<UserInfoDocument> Login(string email, string password)
        {
            Uri uri = new Uri(LOGIN_LINK_TEMPLATE, UriKind.Absolute);

            Dictionary<string, string> postData = new Dictionary<string, string>();
            postData.Add("email", email);
            postData.Add("password", password);

            string json;
            using (HttpClient client = new HttpClient())
            {
                using (HttpContent httpContent = new FormUrlEncodedContent(postData))
                {
                    json = await (await client.PostAsync(uri, httpContent)).Content.ReadAsStringAsync();
                }
            }

            UserInfoDocument document = JsonConvert.DeserializeObject<UserInfoDocument>(json);
            return document;
        }
    }
}