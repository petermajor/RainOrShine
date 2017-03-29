using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RainOrShine
{
	public class CityWeatherQuery : ICityWeatherQuery
	{
		const string UrlFormatString = "weather?id={0}&units={1}&appid={2}";
		const string UnitsMetric = "metric";
		const string UnitsImperial = "imperial";

		static readonly JsonSerializer _serializer = new JsonSerializer();

		readonly IHttpClientProvider _clientProvider;
		readonly IApiKeyProvider _keyProvider;

		public CityWeatherQuery(IHttpClientProvider clientProvider, IApiKeyProvider keyProvider)
		{
			_clientProvider = clientProvider;
			_keyProvider = keyProvider;
		}

		public async Task<WeatherResp> Get(int id, bool metric)
		{
			var url = string.Format(UrlFormatString, id, metric ? UnitsMetric : UnitsImperial, _keyProvider.Get());

			var client = _clientProvider.Get();
			var response = await client.GetAsync(url).ConfigureAwait(false);
			using (response)
			{
				response.EnsureSuccessStatusCode();

				using (var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
				using (var reader = new StreamReader(stream))
				using (var json = new JsonTextReader(reader))
				{
					return _serializer.Deserialize<WeatherResp>(json);
				}
			}
		}
	}
}