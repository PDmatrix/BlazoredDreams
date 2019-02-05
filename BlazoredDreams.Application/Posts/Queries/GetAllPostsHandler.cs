using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BlazoredDreams.Application.Interfaces.DataAccess;
using BlazoredDreams.Domain.Entities;
using MediatR;

namespace BlazoredDreams.Application.Posts.Queries
{
	// ReSharper disable once UnusedMember.Global
	public class GetAllPostsHandler : IRequestHandler<GetAllPosts, IEnumerable<Post>>
	{
		private readonly IUnitOfWork _unitOfWork;

		public GetAllPostsHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		
		public Task<IEnumerable<Post>> Handle(GetAllPosts request, CancellationToken ct)
		{
			return _unitOfWork.PostRepository.GetAllAsync(request.Page, cancellationToken: ct);
		}
	}
}