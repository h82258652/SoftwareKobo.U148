using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SoftwareKobo.U148.Models
{
    [JsonObject]
    public class CommentList : IEnumerable<Comment>
    {
        [JsonProperty("pageNo")]
        public int PageIndex
        {
            get;
            set;
        }

        [JsonProperty("pageSize")]
        public int PageSize
        {
            get;
            set;
        }

        [JsonProperty("pageMax")]
        public int PageCount
        {
            get;
            set;
        }

        [JsonProperty("total")]
        public int Total
        {
            get;
            set;
        }

        [JsonProperty("num")]
        public int Num
        {
            get;
            set;
        }

        [JsonProperty("start")]
        public int Start
        {
            get;
            set;
        }

        [JsonProperty("end")]
        public int End
        {
            get;
            set;
        }

        [JsonProperty("pre")]
        public int Pre
        {
            get;
            set;
        }

        [JsonProperty("next")]
        public int Next
        {
            get;
            set;
        }

        [JsonProperty("data")]
        public Comment[] Data
        {
            get;
            set;
        }

        public IEnumerator<Comment> GetEnumerator()
        {
            return Data.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Data.GetEnumerator();
        }
    }
}