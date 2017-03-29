using System;
using System.Net.Http;

namespace RainOrShine
{
	public class HttpClientProvider : IHttpClientProvider
	{
		static readonly HttpClient _client = new HttpClient { BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/") };

		public HttpClient Get()
		{
			return _client;
		}
	}

	public interface IHttpClientProvider
	{
		HttpClient Get();
	}
}
