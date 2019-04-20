using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BlazoredDreams.Application.Posts.Models;
using Blog.API.Application.Interfaces;
using MediatR;

namespace BlazoredDreams.Application.Posts.Queries
{
	public class GetAllPosts : IRequest<IEnumerable<GetAllPostDto>>
	{
		public int Page { get; set; } = 1;
	}
	
	// ReSharper disable once UnusedMember.Global
	public class GetAllPostsHandler : IRequestHandler<GetAllPosts, IEnumerable<GetAllPostDto>>
	{
		private readonly IUnitOfWork _unitOfWork;

		public GetAllPostsHandler(IUnitOfWorkFactory unitOfWorkFactory)
		{
			_unitOfWork = unitOfWorkFactory.Create();
		}
		
		public async Task<IEnumerable<GetAllPostDto>> Handle(GetAllPosts request, CancellationToken ct)
		{
			await Task.Delay(1);
			return new List<GetAllPostDto>();
			//return _unitOfWork.PostRepository.GetAllPostsAsync(request.Page);
		}
	}
}