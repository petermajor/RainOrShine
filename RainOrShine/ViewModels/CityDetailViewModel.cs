using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using Plugin.Settings.Abstractions;

namespace RainOrShine
{
	public class CityDetailViewModel : MvxViewModel<CityDetailViewModel.NavObj>
	{
		public bool UseMetric { get; set; } = true;

		public string Location { get; set; }

		public string Icon { get; set; }

		public string Temperature { get; set; }

		public string Weather { get; set; }

		public bool IsDefault { get; set; }

		public MvxCommand InvertDefaultCommand { get; private set; }

		readonly ICityWeatherQuery _weatherQuery;

		readonly ISettings _settings;

		int _id;

		public CityDetailViewModel(ICityWeatherQuery weatherQuery, ISettings settings)
		{
			_weatherQuery = weatherQuery;

			_settings = settings;

			InvertDefaultCommand = new MvxCommand(OnInvertDefault);
		}

		protected override async Task Init(NavObj parameter)
		{
			_id = parameter.CityId;

			var result = await _weatherQuery.Get(parameter.CityId, UseMetric);

			BindWeather(result);
		}

		void BindWeather(WeatherResp result)
		{
			if (result == null)
				return;

			Location = result.Name + ", " + result.Sys.Country;

			var weather = result.Weather.FirstOrDefault();
			Icon = weather == null ? null : $"http://openweathermap.org/img/w/{weather.Icon}.png";
			Weather = weather?.Description;

			Temperature = result.Main.Temp.ToString("F1") + " °C";

			var favoriteId = _settings.GetValueOrDefault(Constants.FavoriteCityIdKey, int.MinValue);
			IsDefault = favoriteId == _id;
		}

		void OnInvertDefault()
		{
			IsDefault = !IsDefault;

			if (IsDefault)
			{
				_settings.AddOrUpdateValue(Constants.FavoriteCityIdKey, _id);
				_settings.AddOrUpdateValue(Constants.FavoriteCityNameKey, Location);
			}
			else
			{
				_settings.Remove(Constants.FavoriteCityIdKey);
				_settings.Remove(Constants.FavoriteCityNameKey);
			}
		}

		public class NavObj
		{
			public int CityId { get; set; }
		}
	}
}
