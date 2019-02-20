using System.Collections.Generic;
using BlazoredDreams.Application.Posts.Models;
using BlazorRedux;

namespace BlazoredDreams.App.Redux
{
	public class ChangePageInfo : IAction
	{
		public string Title { get; set; }
		public string BreadCrumb { get; set; }
		public bool OnMainPage { get; set; }
	}

	public class Loading : IAction
	{
		public bool IsLoading;
	}
	
	public class LoadPosts : IAction
	{
		public List<GetAllPostDto> Posts { get; set; }
	}
}