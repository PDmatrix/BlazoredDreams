using System.Collections.Generic;
using BlazoredDreams.Application.Posts.Models;

namespace BlazoredDreams.App.Redux
{
	public class State
	{
		public State()
		{
			PageInfo = new PageInfo
			{
				Title = "WIP",
				BreadCrumb = "WIP"
			};
			Posts = new List<GetAllPostDto>();
		}
		public string Location { get; set; }
		public PageInfo PageInfo { get; set; }
		public List<GetAllPostDto> Posts { get; set; } 
		public bool IsLoading { get; set; }
	}
}