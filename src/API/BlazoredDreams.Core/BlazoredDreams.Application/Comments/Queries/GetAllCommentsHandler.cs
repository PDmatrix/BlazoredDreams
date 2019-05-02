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
		public int PostId { get; set; }
	}
	
	// ReSharper disable once UnusedMember.Global
	public class GetAllCommentsHandler : IRequestHandler<GetAllCommentsQuery, IEnumerable<CommentDto>>
	{
		private readonly IUnitOfWork _unitOfWork;

		public GetAllCommentsHandler(IUnitOfWorkFactory unitOfWorkFactory)
		{
			_unitOfWork = unitOfWorkFactory.Create();
		}
		
		public Task<IEnumerable<CommentDto>> Handle(GetAllCommentsQuery request, CancellationToken ct)
		{
			const string sql =
				@"
				SELECT
					c.id,
					c.content,
				    iu.username,
					c.created_at as date,
				    iu.identifier as user_id,
				    iu.avatar
				FROM comment c
					INNER JOIN identity_user iu on c.user_id = iu.identifier
				WHERE post_id = @postId
				";
			return _unitOfWork.Connection.QueryAsync<CommentDto>(sql, request, _unitOfWork.Transaction);
		}
	}
}