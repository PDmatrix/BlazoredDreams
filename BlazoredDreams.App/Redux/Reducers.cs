using System.Collections.Generic;
using BlazoredDreams.Domain.Entities;
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
				PageInfo = PageInfoReducer(state.PageInfo, action),
				Posts = PostsReducer(state.Posts, action),
				IsLoading = LoadingReducer(state.IsLoading, action)
			};
		}

		private static bool LoadingReducer(bool stateIsLoading, IAction action)
		{
			switch (action)
			{
				case Loading loading:
					return loading.IsLoading;
				default:
					return stateIsLoading;
			}
		}

		private static List<Post> PostsReducer(List<Post> posts, IAction action)
		{
			switch (action)
			{
				case LoadPosts loadedPosts:
					return loadedPosts.Posts;
				default:
					return posts;
			}
		}

		private static PageInfo PageInfoReducer(PageInfo pageInfo, IAction action)
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