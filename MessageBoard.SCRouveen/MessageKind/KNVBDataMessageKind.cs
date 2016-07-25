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
		private readonly string _pathName;
		private readonly string _apiKey;

		protected KNVBDataMessageKind()
		{
			_pathName = "scrouveen";
			_apiKey = "XYIFHe4WKXrQHt9";
		}

		protected string GenerateAbsoluteUrl(string path)
		{
			return GenerateAbsoluteUrl(path, null, string.Empty);
		}

		protected string GenerateAbsoluteUrl(string path, NameValueCollection query, string token)
		{
			var url = string.Concat("http://api.knvbdataservice.nl/api", path);

			if (query == null)
			{
				query = new NameValueCollection();
			}

			if (!string.IsNullOrEmpty(token))
			{
				query.Add("PHPSESSID", token);

				var hash = GenerateHash(string.Concat("#", path, "#"), token);
				query.Add("hash", hash);
			}

			if (query.Count > 0)
			{
				url = string.Concat(url, GenerateQueryString(query));
			}

			return url;
		}

		protected string GenerateInitialization()
		{
			var url = GenerateAbsoluteUrl(string.Concat("/initialisatie/", _pathName));

			dynamic data = ExecuteRequest(url);
			string result = data.List[0].PHPSESSID;
			return result;
		}

		private string CalculateMD5Hash(string input)
		{
			// step 1, calculate MD5 hash from input
			MD5 md5 = System.Security.Cryptography.MD5.Create();
			byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
			byte[] hash = md5.ComputeHash(inputBytes);

			// step 2, convert byte array to hex string
			var sb = new StringBuilder();
			for (int i = 0; i < hash.Length; i++)
			{
				sb.Append(hash[i].ToString("x2"));
			}

			return sb.ToString();
		}

		private string GenerateHash(string hashKey, string token)
		{
			return CalculateMD5Hash(string.Concat(_apiKey, hashKey, token));
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
