using System.Threading.Tasks;
using FluentAssertions;
using MvvmCross.Platform.Platform;
using MvvmCross.Plugins.Json;
using MvvmCross.Test.Core;
using NSubstitute;
using NUnit.Framework;
using Plugin.Settings.Abstractions;

namespace RainOrShine.Test
{
	[TestFixture]
	public class CityDetailViewModelTests : MvxIoCSupportingTest
	{
		WeatherResp AModel = new WeatherResp(123, "London", "GB", "icon_cloudy", "mostly cloudy", 16.5m);

		ISettings _settings;
		ICityWeatherQuery _query;

		[SetUp]
		public void TestSetUp()
		{
			Setup();

			_settings = Substitute.For<ISettings>();
			_settings.GetValueOrDefault(Constants.FavoriteCityIdKey, int.MinValue).Returns(int.MinValue);

			_query = Substitute.For<ICityWeatherQuery>();
			_query.Get(123, true).Returns(Task.FromResult(AModel));

			Ioc.RegisterSingleton<IMvxJsonConverter>(new MvxJsonConverter());
		}

		[Test]
		public async Task WhenInitIsCalledThenWeatherQueryIsCalled()
		{
			var viewmodel = CreateViewModel();

			await viewmodel.Init("{'CityId':123}");

			await _query.Received(1).Get(123, viewmodel.UseMetric);
		}

		[Test]
		public async Task WhenInitIsCalledThenWeatherQueryResultIsBound()
		{
			var viewmodel = CreateViewModel();

			await viewmodel.Init("{'CityId':123}");

			viewmodel.Location.Should().Be("London, GB");
			viewmodel.Icon.Should().Be("http://openweathermap.org/img/w/icon_cloudy.png");
			viewmodel.Temperature.Should().Be("16.5 °C");
			viewmodel.Weather.Should().Be("mostly cloudy");
		}

		[Test]
		public async Task WhenCityIdIsNotDefaultThenIsDefaultIsFalse()
		{
			var viewmodel = CreateViewModel();

			await viewmodel.Init("{'CityId':123}");

			viewmodel.IsDefault.Should().BeFalse();
		}

		[Test]
		public async Task WhenCityIdIsDefaultThenIsDefaultIsTrue()
		{
			_settings.GetValueOrDefault(Constants.FavoriteCityIdKey, int.MinValue).Returns(123);

			var viewmodel = CreateViewModel();

			await viewmodel.Init("{'CityId':123}");

			viewmodel.IsDefault.Should().BeTrue();
		}

		[Test]
		public async Task WhenCityIdDefaultAndButtonClickedThenDefaultRemoved()
		{
			_settings.GetValueOrDefault(Constants.FavoriteCityIdKey, int.MinValue).Returns(123);

			var viewmodel = CreateViewModel();

			await viewmodel.Init("{'CityId':123}");

			viewmodel.InvertDefaultCommand.Execute(null);

			_settings.Received(1).Remove(Constants.FavoriteCityIdKey);
			_settings.Received(1).Remove(Constants.FavoriteCityNameKey);
		}

		[Test]
		public async Task WhenCityIdIsNotDefaultAndButtonClickedThenDefaultAdded()
		{
			var viewmodel = CreateViewModel();

			await viewmodel.Init("{'CityId':123}");

			viewmodel.InvertDefaultCommand.Execute(null);

			_settings.Received(1).AddOrUpdateValue(Constants.FavoriteCityIdKey, 123);
			_settings.Received(1).AddOrUpdateValue(Constants.FavoriteCityNameKey, viewmodel.Location);
		}

		public CityDetailViewModel CreateViewModel()
		{
			return new CityDetailViewModel(_query, _settings);
		}
	}
}

