using Flurl;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EjemploBussiness.RestServices
{
	public abstract class BaseRestService 
	{
		protected abstract string UrlApi { get; }
		protected abstract string EndPoint { get; }
		protected abstract IDictionary<string, string> GlobalHeaders { get; set; }
		protected abstract CookieCollection GlobalCookies { get; set; }
		protected abstract string ContentType { get; }
		protected string BuildUrlApi(params string[] endpoints)
		{
			endpoints = endpoints.Prepend(EndPoint).Prepend(UrlApi).ToArray();
			return Url.Combine(endpoints);
		}

		#region Task Rest Methods
		public Task<HttpResponseMessage> PostTask(string endPoint, NameValueCollection urlParams = null, string body = null)
		{
			return SendTask(HttpMethod.Post, endPoint, urlParams, null, body);
		}

		public Task<HttpResponseMessage> PutTask(string endPoint, NameValueCollection urlParams = null, string body = null)
		{
			return SendTask(HttpMethod.Put, endPoint, urlParams, null, body);
		}

		public Task<HttpResponseMessage> GetTask(string endPoint, NameValueCollection urlParams = null, NameValueCollection queryStrings = null)
		{
			return SendTask(HttpMethod.Get, endPoint, urlParams, queryStrings);
		}
		public Task<HttpResponseMessage> DeleteTask(string endPoint, NameValueCollection urlParams = null, string body = null)
		{
			return SendTask(HttpMethod.Delete, endPoint, urlParams, null, body);
		}

		#endregion

		#region  Task HttpSend Resolve

		public Task<HttpResponseMessage> SendTask(
			HttpMethod httpMethod,
			string endPoint,
			NameValueCollection urlParams = null,
			NameValueCollection queryStrings = null,
			string body = null,
			NameValueCollection headers = null,
			CookieCollection cookies = null
			)
		{
			try
			{
				var uri = new Uri(endPoint);
				if ((urlParams?.Count ?? 0) > 0)
				{
					foreach (var key in urlParams.AllKeys)
					{
						var keyEncode = Url.Encode(string.Concat("{", key, "}"));
						var valueEncode = Url.Encode(urlParams.Get(key));
						endPoint = endPoint.Replace(keyEncode, valueEncode);
					}
					uri = new Uri(endPoint);
				}
				if ((queryStrings?.Count ?? 0) > 0)
				{
					var uriBuilder = new UriBuilder(endPoint);
					var query = HttpUtility.ParseQueryString("");
					query.Add(queryStrings);
					uriBuilder.Query = query.ToString();
					uri = new Uri(uriBuilder.ToString());
				}


				var cookieContainer = new CookieContainer();
				using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
				using (var httpClient = new HttpClient(handler))
				{
					if (GlobalCookies != null)
					{
						cookieContainer.Add(uri, GlobalCookies);
					}
					if (cookies != null)
					{
						cookieContainer.Add(uri, cookies);
					}

					var httpRequestMessage = new HttpRequestMessage(httpMethod, uri);

					if (GlobalHeaders != null)
					{
						foreach (var header in GlobalHeaders)
						{
							httpRequestMessage.Headers.Add(header.Key, header.Value);
						}
					}
					if (headers != null)
					{
						foreach (var key in headers.AllKeys)
						{
							httpRequestMessage.Headers.Add(key, headers.Get(key));
						}
					}

					if (!string.IsNullOrWhiteSpace(body))
					{
						httpRequestMessage.Content = new StringContent(body, Encoding.UTF8, ContentType);
					}
					var httpResponseMessage = httpClient.SendAsync(httpRequestMessage).GetAwaiter().GetResult();
					return Task.FromResult(httpResponseMessage);
				}
			}
			catch (Exception ex)
			{
				throw new HttpRequestException("Error consumiendo servicio rest", ex);
			}

		}
		protected async Task<TType> ReadTask<TType>(HttpResponseMessage responseMessage)
		{
			var content = await responseMessage.Content.ReadAsStringAsync();
			switch (responseMessage.StatusCode)
			{
				case HttpStatusCode.OK:
					{
						var jsonOptions = new JsonSerializerSettings
						{
							PreserveReferencesHandling = PreserveReferencesHandling.All,
							ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
							NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
						};
						jsonOptions.Converters.Add(new StringEnumConverter());
						return JsonConvert.DeserializeObject<TType>(content, jsonOptions);
					}
				case HttpStatusCode.NotFound:
				case HttpStatusCode.NoContent:
					return default(TType);
				default:
					throw new HttpException($"Error al consumir servicio {UrlApi}", new HttpException($"{responseMessage.StatusCode} - {content}"));
			}
		}

		#endregion

	}
}
