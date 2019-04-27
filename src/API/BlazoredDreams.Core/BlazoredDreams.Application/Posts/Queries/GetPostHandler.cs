using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlazoredDreams.Application.Interfaces;
using BlazoredDreams.Application.Posts.Models;
using Dapper;
using MediatR;

namespace BlazoredDreams.Application.Posts.Queries
{
	public class GetPostQuery : IRequest<PostDto>
	{
		public int Id { get; set; }
	}
	
	// ReSharper disable once UnusedMember.Global
	public class GetPostHandler : IRequestHandler<GetPostQuery, PostDto>
	{
		private readonly IUnitOfWork _unitOfWork;

		public GetPostHandler(IUnitOfWorkFactory unitOfWorkFactory)
		{
			_unitOfWork = unitOfWorkFactory.Create();
		}
		
		public Task<PostDto> Handle(GetPostQuery request, CancellationToken ct)
		{
			const string sql = @"
			SELECT DISTINCT
			    p.id,
				iu.username,
				p.title,
				(SELECT COUNT(*) FROM comment c WHERE c.post_id = p.id) as comments,
				to_char(p.created_at, 'YYYY.mm.dd') as date,
				COALESCE(string_agg(t.name, ', ') over (PARTITION BY p.id) , 'Без тега') as tag,
			    d.content   
			FROM post p
				INNER JOIN identity_user iu on p.user_id = iu.identifier
				INNER JOIN dream d on d.id = p.dream_id
				LEFT JOIN post_tags pt on p.id = pt.post_id
				LEFT JOIN tag t on pt.tag_id = t.id
			WHERE p.id = @id
			";

			return _unitOfWork.Connection.QuerySingleOrDefaultAsync<PostDto>(sql, new {request.Id}, _unitOfWork.Transaction);
		}
	}
}