using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BlazoredDreams.Application.Interfaces.DataAccess;
using BlazoredDreams.Application.Posts.Models;
using MediatR;

namespace BlazoredDreams.Application.Posts.Queries
{
	// ReSharper disable once UnusedMember.Global
	public class GetAllPostsHandler : IRequestHandler<GetAllPosts, IEnumerable<GetAllPostDto>>
	{
		private readonly IUnitOfWork _unitOfWork;

		public GetAllPostsHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		
		public Task<IEnumerable<GetAllPostDto>> Handle(GetAllPosts request, CancellationToken ct)
		{
			return _unitOfWork.PostRepository.GetAllPostsAsync(request.Page);
		}
	}
}