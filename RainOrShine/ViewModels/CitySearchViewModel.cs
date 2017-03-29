using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using Plugin.Settings.Abstractions;
using PropertyChanged;

namespace RainOrShine
{
	[ImplementPropertyChanged]
	public class CitySearchViewModel : MvxViewModel
	{
		string _searchTerm;
		public string SearchTerm
		{
			get
			{
				return _searchTerm;
			}
			set
			{
				_searchTerm = value;
				if (string.IsNullOrEmpty(_searchTerm))
					Cities.Clear();
			}
		}

		public bool IsBusy { get; set; }

		public MvxAsyncCommand SearchCommand { get; private set; }

		public MvxCommand<CityListItemViewModel> CitySelectedCommand { get; private set; }

		public MvxObservableCollection<CityListItemViewModel> Cities { get; private set; } 

		readonly ICitySearchQuery _searchQuery;

		readonly ISettings _settings;

		public CitySearchViewModel(ICitySearchQuery searchQuery, ISettings settings)
		{
			_searchQuery = searchQuery;
			_settings = settings;

			SearchCommand = new MvxAsyncCommand(OnSearch);
			CitySelectedCommand = new MvxCommand<CityListItemViewModel>(OnCitySelected);

			Cities = new MvxObservableCollection<CityListItemViewModel>();
		}

		public override void Start()
		{
			base.Start();

			SearchTerm = _settings.GetValueOrDefault<string>(Constants.FavoriteCityNameKey);
		}

		async Task OnSearch()
		{
			IsBusy = true;
			try
			{
				Cities.Clear();

				if (string.IsNullOrWhiteSpace(SearchTerm))
					return;

				var results = await _searchQuery.Get(SearchTerm);

				BindCities(results);
			}
			finally
			{
				IsBusy = false;
			}
		}

		void BindCities(CitySearchResp results)
		{
			var items =
				from i in results.List
				let w = i.Weather.FirstOrDefault()
				select new CityListItemViewModel
				{
					Id = i.Id,
					Location = i.Name + ", " + i.Sys.Country,
					Weather = w?.Description,
					Icon = w == null ? null : $"http://openweathermap.org/img/w/{w.Icon}.png"
				};

			Cities.ReplaceWith(items);
		}

		void OnCitySelected(CityListItemViewModel item)
		{
			if (item == null)
				return;
			
			var nav = new CityDetailViewModel.NavObj { CityId = item.Id };
			ShowViewModel<CityDetailViewModel, CityDetailViewModel.NavObj>(nav);
		}
	}
}
