using System.Threading.Tasks;

namespace RainOrShine
{
	public interface ICitySearchQuery
	{
		Task<CitySearchResp> Get(string query);
	}

	public class CitySearchResp
	{
		public City[] List { get; set; }
	}

	public class City
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public Sys Sys { get; set; }

		public Weather[] Weather { get; set; }
	}
}