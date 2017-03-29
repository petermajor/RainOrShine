using System;
using NUnit.Framework;

// Aliases Func<AppQuery, AppQuery> with Query
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;
using System.Linq;

namespace RainOrShine.UITests
{
	public class CitySearchPage : BasePage
	{
		readonly Query searchEntry;
		readonly Query searchResults;
		readonly Query firstSearchResult;
		readonly Query searchClearButton;

		protected override PlatformQuery Trait => new PlatformQuery
		{
			Android = x => x.Id("searchLayout"),
			iOS = x => x.Id("searchLayout")
		};

		public CitySearchPage()
		{
			if (OnAndroid)
			{
				searchEntry = x => x.Marked("search_src_text");
				searchResults = x => x.Id("thelocation");
				firstSearchResult = x => x.Id("thelocation").Index(0);
				searchClearButton = x => x.Id("search_close_btn");
			}

			if (OniOS)
			{
				throw new NotImplementedException();
			}
		}

		public CitySearchPage SearchForCity(string searchTerm)
		{
			app.WaitForElement(searchEntry);

			app.Screenshot($"Before searching for city: {searchTerm}");
			app.EnterText(searchEntry, searchTerm);
			app.PressEnter();

			return this;
		}

		public CitySearchPage TapSearchClearButton()
		{
			app.WaitForElement(searchClearButton);

			app.Screenshot($"Tapping clear button");
			app.Tap(searchClearButton);

			return this;
		}

		public CitySearchPage TapFirstResult()
		{
			app.WaitForElement(firstSearchResult);

			app.Screenshot($"Tapping first result");
			app.Tap(firstSearchResult);

			return this;
		}

		public CitySearchPage VerifyAtLeastOneResultExists()
		{
			app.WaitForElement(searchResults);

			return this;
		}

		public CitySearchPage VerifyZeroResultExists()
		{
			app.WaitForNoElement(searchResults);

			return this;
		}

		public CitySearchPage VerifyFirstCityLocation(string location)
		{
			if (OnAndroid)
			{
				app.Screenshot($"verifying first result is : {location}");

				var element = app.WaitForElement(firstSearchResult).FirstOrDefault();
				Assert.AreEqual(location, element?.Text);
			}

			if (OniOS)
			{
				throw new NotImplementedException();
			}

			return this;
		}
	}
}