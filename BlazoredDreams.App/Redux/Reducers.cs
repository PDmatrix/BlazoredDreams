using BlazorRedux;

namespace BlazoredDreams.App.Redux
{
	public static class Reducers
	{
		public static State RootReducer(State state, IAction action)
		{
			return new State
			{
				Location = Location.Reducer(state.Location, action),
				PageInfo = PageInfoReducer(state.PageInfo, action)
			};
		}

		public static PageInfo PageInfoReducer(PageInfo pageInfo, IAction action)
		{
			switch (action)
			{
				case ChangePageInfo page:
					return new PageInfo
					{
						Title = page.Title, 
						BreadCrumb = page.BreadCrumb,
						OnMainPage = page.OnMainPage
					};
				default:
					return pageInfo;
			}
		}
	}
}