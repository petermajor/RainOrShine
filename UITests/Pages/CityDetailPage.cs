using System;

// Aliases Func<AppQuery, AppQuery> with Query
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace RainOrShine.UITests
{
	public class CityDetailPage : BasePage
	{
		readonly Func<string, Query> locationLabel;

		protected override PlatformQuery Trait => new PlatformQuery
		{
			Android = x => x.Id("detailLayout"),
			iOS = x => x.Id("detailLayout")
		};

		public CityDetailPage()
		{
			if (OnAndroid)
			{
				locationLabel = location => x => x.Class("Toolbar").Child().Marked(location);
			}

			if (OniOS)
			{
				throw new NotImplementedException();
			}
		}

		public CityDetailPage VerifyLocation(string location)
		{
			if (OnAndroid)
			{
				app.Screenshot($"verifying location : {location}");

				app.WaitForElement(locationLabel(location));
			}

			if (OniOS)
			{
				throw new NotImplementedException();
			}

			return this;
		}
	}
}