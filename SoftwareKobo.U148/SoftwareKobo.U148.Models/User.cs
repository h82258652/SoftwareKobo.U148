using Newtonsoft.Json;

namespace SoftwareKobo.U148.Models
{
    [JsonObject]
    public class User
    {
        [JsonProperty("nickname")]
        public string NickName
        {
            get;
            set;
        }

        [JsonProperty("alias")]
        public string Alias
        {
            get;
            set;
        }
    }
}