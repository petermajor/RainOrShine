using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace RainOrShine
{
	public class App : MvxApplication
	{
		public override void Initialize()
		{
			base.Initialize();

			Mvx.RegisterType<ICitySearchQuery, CitySearchQuery>();
			Mvx.RegisterType<ICityWeatherQuery, CityWeatherQuery>();
			Mvx.RegisterSingleton<ISettings>(CrossSettings.Current);

			RegisterAppStart<CitySearchViewModel>();
		}
	}
}
