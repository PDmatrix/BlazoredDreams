using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BlazoredDreams.Application.Interfaces;
using BlazoredDreams.Application.Posts.Models;
using BlazoredDreams.Application.Shared;
using Dapper;
using MediatR;

namespace BlazoredDreams.Application.Posts.Queries
{
	public class GetAllPostsQuery : IRequest<Page<PostPreviewDto>>
	{
		public int Page { get; set; } = 1;
	}
	
	// ReSharper disable once UnusedMember.Global
	public class GetAllPostsHandler : IRequestHandler<GetAllPostsQuery, Page<PostPreviewDto>>
	{
		private readonly IUnitOfWork _unitOfWork;

		public GetAllPostsHandler(IUnitOfWorkFactory unitOfWorkFactory)
		{
			_unitOfWork = unitOfWorkFactory.Create();
		}

		private Task<IEnumerable<PostPreviewDto>> GetPosts(int page, int pageSize)
		{
			const string sql = @"
			SELECT DISTINCT
			  p.id,
			  iu.username,
			  p.title,
			  (SELECT COUNT(*) FROM comment c WHERE c.post_id = p.id) as comments,
			  p.excerpt,
			  p.created_at as date,
			  COALESCE(string_agg(t.name, ',') over (PARTITION BY p.id) , 'Без тега') as tag
			FROM post p
				   INNER JOIN identity_user iu on p.user_id = iu.identifier
				   INNER JOIN dream d on d.id = p.dream_id
				   LEFT JOIN post_tags pt on p.id = pt.post_id
				   LEFT JOIN tag t on pt.tag_id = t.id
			ORDER BY p.created_at desc
			LIMIT @pageSize OFFSET @page
			";
			var sqlParam = new
			{
				pageSize,
				page = (page - 1) * pageSize
			};
			return _unitOfWork.Connection.QueryAsync<PostPreviewDto>(sql, sqlParam, _unitOfWork.Transaction);
		}
		
		private Task<int> GetTotalPages(int pageSize)
		{
			const string sql = @"
			SELECT
				ceil(count(*) / cast(@pageSize as float)) as total_pages
			FROM post p
				INNER JOIN identity_user iu on p.user_id = iu.identifier
				INNER JOIN dream d on d.id = p.dream_id
			";
			return _unitOfWork.Connection.QuerySingleOrDefaultAsync<int>(sql, new {pageSize}, _unitOfWork.Transaction);
		}
		
		public async Task<Page<PostPreviewDto>> Handle(GetAllPostsQuery request, CancellationToken ct)
		{
			const int pageSize = 10;
			var records = await GetPosts(request.Page, pageSize);
			var totalPages = await GetTotalPages(pageSize);
			return new Page<PostPreviewDto>
			{
				Records = records,
				CurrentPage = request.Page,
				PageSize = pageSize,
				TotalPages = totalPages
			};
		}
	}
}