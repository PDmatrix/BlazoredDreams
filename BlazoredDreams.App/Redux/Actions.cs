using BlazorRedux;

namespace BlazoredDreams.App.Redux
{
	public class ChangePageInfo : IAction
	{
		public string Title { get; set; }
		public string BreadCrumb { get; set; }
	}
}