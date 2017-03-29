using System;
using System.Windows.Input;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Platform.WeakSubscription;

namespace MvvmCross.Binding.Droid.Target
{
	public class MvxSearchViewQueryTextSubmitBinding : MvxAndroidTargetBinding
	{
		public MvxSearchViewQueryTextSubmitBinding(View view)
			: base(view)
		{
			_subscription = SearchView.WeakSubscribe<SearchView, SearchView.QueryTextSubmitEventArgs>(
				nameof(SearchView.QueryTextSubmit),
				HandleQueryTextSubmit);
		}

		IDisposable _subscription;

		public override Type TargetType => typeof(ICommand);

		public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;

		protected SearchView SearchView => (SearchView)Target;

		ICommand _command;

		protected override void SetValueImpl(object target, object value)
		{
			_command = value as ICommand;
		}

		protected override void Dispose(bool isDisposing)
		{
			if (isDisposing)
			{
				_subscription?.Dispose();
				_subscription = null;
			}

			base.Dispose(isDisposing);
		}

		void HandleQueryTextSubmit(object sender, SearchView.QueryTextSubmitEventArgs e)
		{
			if (_command == null)
				return;

			if (!_command.CanExecute(null))
				return;

			SearchView.ClearFocus();
			_command.Execute(null);
		}
	}
}