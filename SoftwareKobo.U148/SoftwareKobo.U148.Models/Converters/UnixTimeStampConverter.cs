using Newtonsoft.Json;
using System;
using System.Globalization;

namespace SoftwareKobo.U148.Models.Converters
{
    public class UnixTimeStampConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string strUnixTimeStamp = (string)reader.Value;
            int unixTimeStamp;
            if (int.TryParse(strUnixTimeStamp, out unixTimeStamp) == false)
            {
                throw new JsonException(string.Format(CultureInfo.InvariantCulture, "could not convert unix time stamp {0}.", strUnixTimeStamp));
            }

            DateTime utcDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            utcDateTime = utcDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return utcDateTime;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}