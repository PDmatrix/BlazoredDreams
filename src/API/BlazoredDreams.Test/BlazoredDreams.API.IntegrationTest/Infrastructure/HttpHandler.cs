using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BlazoredDreams.API.IntegrationTest.Infrastructure
{
	public class HttpHandler
	{
		private readonly HttpClient _client;
		
		public HttpHandler(HttpClient client)
		{
			_client = client;
		}

		public async Task<T> CallAsync<T>(HttpRequestMessage requestMessage)
		{
			var response = await SendHttpRequestMessageAsync(requestMessage);
			var stream = await response.Content.ReadAsStreamAsync();
			return DeserializeJsonFromStream<T>(stream);
		}
		
		public Task CallAsync(HttpRequestMessage requestMessage)
        {
            return SendHttpRequestMessageAsync(requestMessage);
        }

		private async Task<HttpResponseMessage> SendHttpRequestMessageAsync(HttpRequestMessage requestMessage)
		{
			var response = await _client.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead);
			response.EnsureSuccessStatusCode();
			return response;
		}

		public static HttpRequestMessage CreateHttpRequestMessage<T>(HttpMethod httpMethod, T content, string url)
		{
			return new HttpRequestMessage(httpMethod, url)
			{
				Content = new ObjectContent<T>(content, new JsonMediaTypeFormatter(), (MediaTypeHeaderValue) null)
			};
		}
		
		public static HttpRequestMessage CreateHttpRequestMessage(HttpMethod httpMethod, string url)
		{
			return new HttpRequestMessage(httpMethod, url);
		}
	
		private static T DeserializeJsonFromStream<T>(Stream stream)
		{
			if (stream == null || stream.CanRead == false)
				return default;

			using (var sr = new StreamReader(stream))
			using (var jtr = new JsonTextReader(sr))
			{
				var js = new JsonSerializer();
				var searchResult = js.Deserialize<T>(jtr);
				return searchResult;
			}
		}	
	}
}