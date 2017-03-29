using System.Collections.Generic;
using System.Reflection;
using Android.Content;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Binding.Droid.Target;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Platform;
using MvvmCross.Droid.Support.V7.AppCompat;
using SearchView = Android.Support.V7.Widget.SearchView;

namespace RainOrShine.Droid
{
	public class Setup : MvxAndroidSetup
	{
		public Setup(Context applicationContext) : base(applicationContext)
		{
		}

		protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
		{
			MvxAppCompatSetupHelper.FillTargetFactories(registry);

			base.FillTargetFactories(registry);

			registry.RegisterCustomBindingFactory<SearchView>("QueryTextSubmit", view => new MvxSearchViewQueryTextSubmitBinding(view));
		}

		protected override IEnumerable<Assembly> AndroidViewAssemblies
		{
			get
			{
				var assemblies = new List<Assembly>(base.AndroidViewAssemblies)
					{
						typeof(Android.Support.V7.Widget.CardView).Assembly,
						typeof(Android.Support.V7.Widget.Toolbar).Assembly,
						typeof(MvvmCross.Droid.Support.V7.RecyclerView.MvxRecyclerView).Assembly
					};
				return assemblies;
			}
		}

		protected override IMvxApplication CreateApp()
		{
			return new App();
		}
	}
}