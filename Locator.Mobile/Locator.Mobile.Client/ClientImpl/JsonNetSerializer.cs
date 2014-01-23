using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Locator.Mobile.BL.Client;
using Newtonsoft.Json;

namespace Locator.Mobile.Client.ClientImpl
{
    /// <summary>
    /// Сериализатор Newtonsoft.JsonNet
    /// </summary>
    public class JsonNetSerializer : ISerializer, RestSharp.Serializers.ISerializer
    {
        private readonly JsonConverter[] converters;

        public JsonNetSerializer(IEnumerable<JsonConverter> converters)
        {
            this.converters = converters.ToArray();
        }

        public T Deserialize<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content, converters);
        }

        public string Serialize(object obj)
        {
			return JsonConvert.SerializeObject (obj);//, new JsonSerializerSettings() { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat });
        }

        public string RootElement { get; set; }
        public string Namespace { get; set; }
        public string DateFormat { get; set; }
        private string contentType = "application/json";
        public string ContentType { get { return contentType; } set { contentType = value; } }
    }
}
