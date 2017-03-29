using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RainOrShine
{
	public class CitySearchQuery : ICitySearchQuery
	{
		const string UrlFormatString = "http://api.openweathermap.org/data/2.5/find?q={0}&type=like&sort=population&cnt=30&appid=e28321d706d9d70546427dc0f2942622";

		static readonly HttpClient _client = new HttpClient();
		static readonly JsonSerializer _serializer = new JsonSerializer();

		public async Task<CitySearchResp> Get(string query)
		{
			var url = string.Format(UrlFormatString, query);

			var response = await _client.GetAsync(url).ConfigureAwait(false);
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
