namespace BlazoredDreams.App.Redux
{
	public class State
	{
		public State()
		{
			PageInfo = new PageInfo {Title = "WIP", BreadCrumb = "WIP"};
		}
		public string Location { get; set; }
		public PageInfo PageInfo { get; set; }
	}
}