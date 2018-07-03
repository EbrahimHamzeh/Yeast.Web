using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yeast.Utilities.IPLocate
{
    public class IPLocateViewModel
    {
        public string ip { get; set; }
        public string country { get; set; }
        public string country_code { get; set; }
        public string continent { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string time_zone { get; set; }
        public string postal_code { get; set; }
        public string org { get; set; }
        public string asn { get; set; }
        public string subdivision { get; set; }
        public string subdivision2 { get; set; }
    }

    public static class IPLocateWrapper
    {
        public static IPLocateViewModel DeserializeObject(string json)
        {
            Newtonsoft.Json.JsonSerializerSettings setting = new Newtonsoft.Json.JsonSerializerSettings();
            setting.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            setting.MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore;
            setting.FloatParseHandling = Newtonsoft.Json.FloatParseHandling.Decimal;
            setting.Error = FillDetailsHandleDeserializationError;

            return Newtonsoft.Json.JsonConvert.DeserializeObject<IPLocateViewModel>(json, setting);
        }

        public static IPLocateViewModel HttpClient(string ip)
        {
            var host = new Uri("https://www.iplocate.io/");
            var httpClient = LazySingletonHttpClient.Instance.GetOrCreate(host);
            var responseMessage = httpClient.GetAsync("api/lookup/" + ip).Result;
            var responseContent = responseMessage.Content.ReadAsStringAsync().Result;
            return DeserializeObject(responseContent);
        }

        public static void FillDetailsHandleDeserializationError(object sender, ErrorEventArgs errorArgs)
        {
            var currentError = errorArgs.ErrorContext.Error.Message;
            errorArgs.ErrorContext.Handled = true;
        }
    }


}
