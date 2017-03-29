using Android.App;
using Android.Content.PM;
using MvvmCross.Droid.Views;

namespace RainOrShine.Droid.Views
{
	[Activity(
		Label = "Rain Or Shine"
		, MainLauncher = true
		, Icon = "@mipmap/ic_launcher"
		, Theme = "@style/Theme.RainOrShine.Splash"
		, NoHistory = true
		, ScreenOrientation = ScreenOrientation.Portrait)]
	public class SplashView : MvxSplashScreenActivity
	{
		protected override void OnCreate(Android.OS.Bundle bundle)
		{
			base.OnCreate(bundle);
		}
	}
}