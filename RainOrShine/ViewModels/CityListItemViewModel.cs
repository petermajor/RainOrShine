using PropertyChanged;

namespace RainOrShine
{
	[ImplementPropertyChanged]
	public class CityListItemViewModel
	{
		public int Id { get; set; }
		public string Location { get; set; }
		public string Weather { get; set; }
		public string Icon { get; set; }
	}
}
