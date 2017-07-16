using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MessageBoard.SCRouveen.MessageKind
{
	public abstract class KNVBDataMessageKind : Core.MessageKind.MessageKind
	{
		public const string BooleanYes = "JA";
		public const string BooleanNo = "NEE";

		private readonly string _apiClientId;

		protected KNVBDataMessageKind()
		{
			_apiClientId = "LBh05irDly";
		}

		protected string GenerateAbsoluteUrl(string path)
		{
			return GenerateAbsoluteUrl(path, null);
		}

		protected string GenerateAbsoluteUrl(string path, NameValueCollection query)
		{
			var url = string.Concat("https://data.sportlink.com", path);

			if (query == null)
			{
				query = new NameValueCollection();
			}

			query.Add("clientId", _apiClientId);			

			if (query.Count > 0)
			{
				url = string.Concat(url, GenerateQueryString(query));
			}

			return url;
		}

		private string GenerateQueryString(NameValueCollection nvc)
		{
			if (nvc == null || nvc.Count == 0)
			{
				return string.Empty;
			}

			var array = (from key in nvc.AllKeys
									 from value in nvc.GetValues(key)
									 select string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value)))
					.ToArray();

			return "?" + string.Join("&", array);
		}

		protected object ExecuteRequest(string url)
		{
			object result = null;
			using (var httpClient = new HttpClient())
			{
				var task = httpClient
					.GetAsync(url)
					.ContinueWith((taskWithResponse) =>
					{
						var response = taskWithResponse.Result;
						var jsonData = response.Content.ReadAsStringAsync();
						jsonData.Wait();
						result = JsonConvert.DeserializeObject(jsonData.Result);
					});

				task.Wait();
			}

			return result;
		}
	}
}
