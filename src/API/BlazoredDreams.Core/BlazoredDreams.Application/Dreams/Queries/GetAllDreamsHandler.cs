using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BlazoredDreams.Application.Dreams.Models;
using BlazoredDreams.Application.Interfaces;
using BlazoredDreams.Application.Shared;
using Dapper;
using MediatR;

namespace BlazoredDreams.Application.Dreams.Queries
{
	public class GetAllDreamsQuery : IRequest<Page<DreamDto>>
	{
		public int Page { get; set; } = 1;
		public string UserId { get; set; }
	}
	
	// ReSharper disable once UnusedMember.Global
	public class GetAllDreamsHandler : IRequestHandler<GetAllDreamsQuery, Page<DreamDto>>
	{
		private readonly IUnitOfWork _unitOfWork;

		public GetAllDreamsHandler(IUnitOfWorkFactory unitOfWorkFactory)
		{
			_unitOfWork = unitOfWorkFactory.Create();
		}
		
		public async Task<Page<DreamDto>> Handle(GetAllDreamsQuery request, CancellationToken ct)
		{
			const int pageSize = 10;
			var records = await GetDreams(request.UserId, request.Page, pageSize);
			var totalPages = await GetTotalPages(request.UserId, pageSize);
			return new Page<DreamDto>
			{
				Records = records,
				CurrentPage = request.Page,
				PageSize = pageSize,
				TotalPages = totalPages
			};
		}
		
		private Task<IEnumerable<DreamDto>> GetDreams(string userId, int page, int pageSize)
		{
			const string sql =
				@"
				SELECT
				   d.id,
				   iu.username,
				   d.content,
				   to_char(d.created_at, 'YYYY.mm.dd') as date,
				   p.title IS NOT NULL as is_published
				FROM dream d
				   INNER JOIN identity_user iu on d.user_id = iu.identifier
				   LEFT JOIN post p on d.id = p.dream_id
				WHERE d.user_id = @userId
				ORDER BY d.created_at desc 
				LIMIT @pageSize OFFSET @page
				";
			var sqlParam = new
			{
				pageSize,
				page = (page - 1) * pageSize,
				userId
			};
			
			return _unitOfWork.Connection.QueryAsync<DreamDto>(sql, sqlParam, _unitOfWork.Transaction);
		}
		
		private Task<int> GetTotalPages(string userId, int pageSize)
		{
			const string sql = @"
			SELECT
			    ceil(count(*) / cast(@pageSize as float)) as total_pages
			FROM dream d
			WHERE d.user_id = @userId
			";
			var sqlParams = new
			{
				pageSize,
				userId
			};
			return _unitOfWork.Connection.QuerySingleOrDefaultAsync<int>(sql, sqlParams, _unitOfWork.Transaction);
		}
	}
}