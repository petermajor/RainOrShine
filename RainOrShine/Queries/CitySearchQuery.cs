using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RainOrShine
{
	public class CitySearchQuery : ICitySearchQuery
	{
		const string UrlFormatString = "find?q={0}&type=like&sort=population&cnt=30&appid={1}";

		static readonly JsonSerializer _serializer = new JsonSerializer();

		readonly IHttpClientProvider _clientProvider;
		readonly IApiKeyProvider _keyProvider;

		public CitySearchQuery(IHttpClientProvider clientProvider, IApiKeyProvider keyProvider)
		{
			_clientProvider = clientProvider;
			_keyProvider = keyProvider;
		}

		public async Task<CitySearchResp> Get(string query)
		{
			var url = string.Format(UrlFormatString, query, _keyProvider.Get());

			var client = _clientProvider.Get();
			var response = await client.GetAsync(url).ConfigureAwait(false);
			using (response)
			{
				response.EnsureSuccessStatusCode();

				using (var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
				using (var reader = new StreamReader(stream))
				using (var json = new JsonTextReader(reader))
				{
					return _serializer.Deserialize<CitySearchResp>(json);
				}
			}
		}
	}
}
