using System.Linq;
using System.Threading.Tasks;
using BlazoredDreams.Application.Posts.Queries;
using BlazorRedux;
using MediatR;

namespace BlazoredDreams.App.Redux
{
	public static class ActionCreators
	{
		public static async Task FetchPosts(Dispatcher<IAction> dispatcher, IMediator mediator, int page = 1)
		{
			dispatcher(new Loading {IsLoading = true});
			var posts = await mediator.Send(new GetAllPosts{Page = page});
			dispatcher(new LoadPosts {Posts = posts.ToList()});
			dispatcher(new Loading {IsLoading = false});
		}
	}
}