using Newtonsoft.Json;

namespace SoftwareKobo.U148.Models
{
    [JsonObject]
    public class CommentDocument
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
        public CommentList Data
        {
            get;
            set;
        }
    }
}