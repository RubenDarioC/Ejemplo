using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EjemploBussiness.RestServices
{
	public class EjemploBaseRest : BaseRestService
	{
		protected override string UrlApi => throw new NotImplementedException();

		protected override string EndPoint => throw new NotImplementedException();

		protected override IDictionary<string, string> GlobalHeaders { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		protected override CookieCollection GlobalCookies { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		protected override string ContentType => throw new NotImplementedException();


		public string PostBody(string body)
		{
			var url = BuildUrlApi("Login");
			var bodyConvert = JsonConvert.SerializeObject(body);
			var response = PostTask(url, null, body).Result;
			if (response.StatusCode != HttpStatusCode.OK) return "Error";
			var result = ReadTask<string>(response).Result;
			return result;
		}
		public string GetByQueryString(string id)
		{
			var url = BuildUrlApi("endpoint/GetById");
			var queryString = new NameValueCollection();
			queryString.Add("id", id);
			var response = GetTask(url, null, queryString).Result; // 'https://localhost:90/api/endpoint/GetById?id=10'
			if (response.StatusCode != HttpStatusCode.OK) return "Error";
			var result = ReadTask<string>(response);
			return result.Result;
		}
		public string GetByParameters(string id)
		{
			var url = BuildUrlApi("endpoint/{id}");
			var urlParameters = new NameValueCollection();
			urlParameters.Add("id", id);
			var response = GetTask(url, null, urlParameters).Result; // 'https://localhost:90/api/endpoint/10'
			if (response.StatusCode != HttpStatusCode.OK) return "Error";
			var result = ReadTask<string>(response);
			return result.Result;
		}



	}
}
