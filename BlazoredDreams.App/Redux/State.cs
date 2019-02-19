using System.Collections.Generic;
using BlazoredDreams.Domain.Entities;

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
			Posts = new List<Post>();
		}
		public string Location { get; set; }
		public PageInfo PageInfo { get; set; }
		public List<Post> Posts { get; set; } 
		public bool IsLoading { get; set; }
	}
}