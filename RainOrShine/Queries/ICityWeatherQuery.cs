using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RainOrShine
{
	public interface ICityWeatherQuery
	{
		Task<WeatherResp> Get(int id, bool metric);
	}

	public class WeatherResp
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public Weather[] Weather { get; set; }
		public Main Main { get; set; }
		public Sys Sys { get; set; }

		public WeatherResp()
		{
		}

		public WeatherResp(int id, string name, string country, string weatherIcon, string weatherDesc, decimal mainTemp)
		{
			Id = id;
			Name = name;
			Sys = new Sys { Country = country };
			Weather = new[] { new Weather { Icon = weatherIcon, Description = weatherDesc } };
			Main = new Main { Temp = mainTemp };
		}
	}

	public class Weather
	{
		public string Main { get; set; }
		public string Description { get; set; }
		public string Icon { get; set; }
	}

	public class Sys
	{
		public string Country { get; set; }
	}

	public class Main
	{
		public decimal Temp { get; set; }
		[JsonProperty("temp_min")]
		public decimal Min { get; set; }
		[JsonProperty("temp_max")]
		public decimal Max { get; set; }
	}
}
