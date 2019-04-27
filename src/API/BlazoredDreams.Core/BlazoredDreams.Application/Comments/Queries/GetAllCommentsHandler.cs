using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BlazoredDreams.Application.Comments.Models;
using BlazoredDreams.Application.Interfaces;
using BlazoredDreams.Application.Posts.Models;
using BlazoredDreams.Application.Posts.Queries;
using BlazoredDreams.Application.Shared;
using Dapper;
using MediatR;

namespace BlazoredDreams.Application.Comments.Queries
{
	public class GetAllCommentsQuery : IRequest<IEnumerable<CommentDto>>
	{
		public int Page { get; set; } = 1;
	}
	
	// ReSharper disable once UnusedMember.Global
	public class GetAllCommentsHandler : IRequestHandler<GetAllCommentsQuery, IEnumerable<CommentDto>>
	{
		private readonly IUnitOfWork _unitOfWork;

		public GetAllCommentsHandler(IUnitOfWorkFactory unitOfWorkFactory)
		{
			_unitOfWork = unitOfWorkFactory.Create();
		}
		
		public async Task<IEnumerable<CommentDto>> Handle(GetAllCommentsQuery request, CancellationToken ct)
		{
			
		}
	}
}