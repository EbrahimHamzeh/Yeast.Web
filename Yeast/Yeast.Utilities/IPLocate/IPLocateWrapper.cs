using Elmah;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace Yeast.Utilities.IPLocate
{
	public class IPLocateViewModel
	{
		public string ipAddress { get; set; }
		public string statusCode { get; set; }
		public string statusMessage { get; set; }
		public string countryCode { get; set; }
		public string countryName { get; set; }
	}

	public static class IPLocateWrapper
	{
		private const string Key = "82101a4ab2d659e6d8c5846b31f5ee4c80ebb0b5430bf7f65c01ff302dc6faaf";
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
			try
			{
				var host = new Uri("http://api.ipinfodb.com/");
				var httpClient = LazySingletonHttpClient.Instance.GetOrCreate(host);
				var responseMessage = httpClient.GetAsync($"v3/ip-country/?ip={ip}&key={Key}&format=json").Result;
				var responseContent = responseMessage.Content.ReadAsStringAsync().Result;

				System.IO.File.AppendAllText($"{HostingEnvironment.ApplicationPhysicalPath}/ApiLog.txt",
					"Response = " + $"{responseContent}" + Environment.NewLine +
					"-----------------------------" + DateTime.Now + "-----------------------------" + Environment.NewLine,
					System.Text.ASCIIEncoding.UTF8);

				return DeserializeObject(responseContent);
			}
			catch (Exception ex)
			{
				ErrorSignal.FromCurrentContext().Raise(ex);
				return null;
			}
		}

		public static void FillDetailsHandleDeserializationError(object sender, ErrorEventArgs errorArgs)
		{
			var currentError = errorArgs.ErrorContext.Error.Message;
			errorArgs.ErrorContext.Handled = true;
		}
	}


}
