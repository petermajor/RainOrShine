using NUnit.Framework;
using Xamarin.UITest;

namespace RainOrShine.UITests
{
	public class RainOrShineTests : BaseTestFixture
	{
		public RainOrShineTests(Platform platform)
			: base(platform)
		{
		}

		[Test]
		public void SearchForCity()
		{
			new CitySearchPage()
				.SearchForCity("London, GB")
				.VerifyFirstCityLocation("London, GB");
		}

		[Test]
		public void ClearingSearchTermClearsSearchResults()
		{
			new CitySearchPage()
				.SearchForCity("London, GB")
				.VerifyAtLeastOneResultExists()
				.TapSearchClearButton()
				.VerifyZeroResultExists();
		}

		[Test]
		public void TappingOnSearchResultShowsDetailPage()
		{
			new CitySearchPage()
				.SearchForCity("London, GB")
				.TapFirstResult();

			new CityDetailPage()
				.VerifyLocation("London, GB");
		}
	}
}