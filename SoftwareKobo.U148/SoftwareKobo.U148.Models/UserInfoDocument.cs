using Newtonsoft.Json;

namespace SoftwareKobo.U148.Models
{
    [JsonObject]
    public class UserInfoDocument
    {
        [JsonProperty("code")]
        public int Code
        {
            get;
            set;
        }

        [JsonProperty("msg")]
        public string Message
        {
            get;
            set;
        }

        [JsonProperty("data")]
        public UserInfo Data
        {
            get;
            set;
        }
    }
}