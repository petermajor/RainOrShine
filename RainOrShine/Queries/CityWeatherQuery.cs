using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RainOrShine
{
	public class CityWeatherQuery : ICityWeatherQuery
	{
		const string UrlFormatString = "http://api.openweathermap.org/data/2.5/weather?id={0}&units={1}&appid=e28321d706d9d70546427dc0f2942622";
		const string UnitsMetric = "metric";
		const string UnitsImperial = "imperial";

		static readonly HttpClient _client = new HttpClient();
		static readonly JsonSerializer _serializer = new JsonSerializer();

		public async Task<WeatherResp> Get(int id, bool metric)
		{
			var url = string.Format(UrlFormatString, id, metric ? UnitsMetric : UnitsImperial);

			var response = await _client.GetAsync(url).ConfigureAwait(false);
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