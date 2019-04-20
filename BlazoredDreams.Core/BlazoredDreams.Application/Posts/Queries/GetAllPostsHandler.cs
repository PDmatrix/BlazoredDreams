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
	public class GetAllPostsQuery : IRequest<IEnumerable<PostPreviewDto>>
	{
		public int Page { get; set; } = 1;
	}
	
	// ReSharper disable once UnusedMember.Global
	public class GetAllPostsHandler : IRequestHandler<GetAllPostsQuery, IEnumerable<PostPreviewDto>>
	{
		private readonly IUnitOfWork _unitOfWork;

		public GetAllPostsHandler(IUnitOfWorkFactory unitOfWorkFactory)
		{
			_unitOfWork = unitOfWorkFactory.Create();
		}
		
		public Task<IEnumerable<PostPreviewDto>> Handle(GetAllPostsQuery request, CancellationToken ct)
		{
			const string sql = @"
			SELECT
				iu.identifier as username,
				p.title,
				(SELECT COUNT(*) FROM comment c WHERE c.post_id = p.id) as comments,
				p.excerpt,
				to_char(p.created_at, 'YYYY.mm.dd') as date,
				COALESCE(t.name, 'Без тега') as tag,
				ceil(cast(count(*) over() as float) / cast(@pageSize as float)) as total_pages
			FROM post p
				INNER JOIN identity_user iu on p.user_id = iu.id
				INNER JOIN dream d on d.id = p.dream_id
				LEFT JOIN post_tags pt on p.id = pt.post_id
				LEFT JOIN tag t on pt.tag_id = t.id
			LIMIT @pageSize OFFSET @page
			";
			const int pageSize = 10;
			var sqlParam = new
			{
				pageSize,
				page = (request.Page - 1) * pageSize
			};
			
			return _unitOfWork.Connection.QueryAsync<PostPreviewDto>(sql, sqlParam, _unitOfWork.Transaction);
		}
	}
}