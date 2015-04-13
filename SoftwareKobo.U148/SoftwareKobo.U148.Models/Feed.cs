using Newtonsoft.Json;
using SoftwareKobo.U148.Models.Converters;
using System;

namespace SoftwareKobo.U148.Models
{
    public class Feed
    {
        [JsonProperty("id")]
        public int Id
        {
            get;
            set;
        }

        [JsonProperty("uid")]
        public int Uid
        {
            get;
            set;
        }

        [JsonProperty("category")]
        public FeedCategory Category
        {
            get;
            set;
        }

        [JsonProperty("title")]
        public string Title
        {
            get;
            set;
        }

        [JsonProperty("summary")]
        public string Summary
        {
            get;
            set;
        }

        [JsonProperty("tids")]
        public string Tids
        {
            get;
            set;
        }

        [JsonProperty("tags")]
        public string Tags
        {
            get;
            set;
        }

        [JsonProperty("pic_min")]
        public string PicMin
        {
            get;
            set;
        }

        [JsonProperty("pic_mid")]
        public string PicMid
        {
            get;
            set;
        }

        [JsonProperty("is_index")]
        public int IsIndex
        {
            get;
            set;
        }

        [JsonProperty("is_hot")]
        public int IsHot
        {
            get;
            set;
        }

        [JsonProperty("is_top")]
        public int IsTop
        {
            get;
            set;
        }

        [JsonProperty("star")]
        public int Star
        {
            get;
            set;
        }

        [JsonProperty("count_browse")]
        public int CountBrowse
        {
            get;
            set;
        }

        [JsonProperty("count_review")]
        public int CountReview
        {
            get;
            set;
        }

        [JsonProperty("create_time")]
        [JsonConverter(typeof(UnixTimeStampConverter))]
        public DateTime CreateTime
        {
            get;
            set;
        }

        [JsonProperty("update_time")]
        [JsonConverter(typeof(UnixTimeStampConverter))]
        public DateTime UpdateTime
        {
            get;
            set;
        }

        [JsonProperty("start_time")]
        [JsonConverter(typeof(UnixTimeStampConverter))]
        public DateTime StartTime
        {
            get;
            set;
        }

        [JsonProperty("usr")]
        public User User
        {
            get;
            set;
        }
    }
}

//public class Rootobject
//{
//    public int code { get; set; }
//    public string msg { get; set; }
//    public Data data { get; set; }
//}

//public class Data
//{
//    public int pageNo { get; set; }
//    public int pageSize { get; set; }
//    public int pageMax { get; set; }
//    public int total { get; set; }
//    public int num { get; set; }
//    public int start { get; set; }
//    public int end { get; set; }
//    public int pre { get; set; }
//    public int next { get; set; }
//    public Datum[] data { get; set; }
//}

//public class Datum
//{
//    public int id { get; set; }
//    public int uid { get; set; }
//    public int category { get; set; }
//    public string title { get; set; }
//    public string summary { get; set; }
//    public string tids { get; set; }
//    public string tags { get; set; }
//    public string pic_min { get; set; }
//    public string pic_mid { get; set; }
//    public int is_index { get; set; }
//    public int is_hot { get; set; }
//    public int is_top { get; set; }
//    public int star { get; set; }
//    public int count_browse { get; set; }
//    public int count_review { get; set; }
//    public string create_time { get; set; }
//    public string update_time { get; set; }
//    public string start_time { get; set; }
//    public Usr usr { get; set; }
//}

//public class Usr
//{
//    public string nickname { get; set; }
//    public string alias { get; set; }
//}