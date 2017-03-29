using Android.App;
using Android.OS;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.AppCompat;
using Android.Widget;
using Android.Support.V7.Content.Res;

namespace RainOrShine.Droid
{
	[Activity]
	public class CityDetailView : MvxAppCompatActivity<CityDetailViewModel>
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			SetContentView(Resource.Layout.CityDetailView);

			SupportActionBar.SetDisplayHomeAsUpEnabled(true);
			SupportActionBar.SetDisplayShowHomeEnabled(true);

			this.CreateBinding().For("Title").To<CityDetailViewModel>(vm => vm.Location).Apply();
		}

		public override bool OnOptionsItemSelected(Android.Views.IMenuItem item)
		{
			if (item.ItemId == Android.Resource.Id.Home)
			{
				Finish();
				return true;
			}

			return base.OnOptionsItemSelected(item);
		}
	}
}