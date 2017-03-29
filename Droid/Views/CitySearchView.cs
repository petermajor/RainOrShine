using Android.App;
using Android.OS;
using Android.Views;
using MvvmCross.Droid.Support.V7.AppCompat;
using SearchView = Android.Support.V7.Widget.SearchView;

namespace RainOrShine.Droid
{
	[Activity(Label = "Rain Or Shine")]
	public class CitySearchView : MvxAppCompatActivity<CitySearchViewModel>
	{
		const bool SubmitTrue = true;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			SetContentView(Resource.Layout.CitySearchView);

			var searchView = FindViewById<SearchView>(Resource.Id.search);
			var plate = searchView.FindViewById<View>(Resource.Id.search_plate);
			plate.SetBackgroundResource(Resource.Color.search_background);

			if (!string.IsNullOrEmpty(ViewModel.SearchTerm))
				searchView.SetQuery(ViewModel.SearchTerm, SubmitTrue);
		}
	}
}