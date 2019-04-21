using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BlazoredDreams.Application.Dreams.Models;
using BlazoredDreams.Application.Interfaces;
using Dapper;
using MediatR;

namespace BlazoredDreams.Application.Dreams.Queries
{
	public class GetAllDreamsQuery : IRequest<IEnumerable<DreamDto>>
	{
		public int Page { get; set; } = 1;
	}
	
	// ReSharper disable once UnusedMember.Global
	public class GetAllDreamsHandler : IRequestHandler<GetAllDreamsQuery, IEnumerable<DreamDto>>
	{
		private readonly IUnitOfWork _unitOfWork;

		public GetAllDreamsHandler(IUnitOfWorkFactory unitOfWorkFactory)
		{
			_unitOfWork = unitOfWorkFactory.Create();
		}
		
		public Task<IEnumerable<DreamDto>> Handle(GetAllDreamsQuery request, CancellationToken ct)
		{
			const string sql =
				@"
				SELECT
				    d.id,
					iu.identifier as username,
				    d.content,
					to_char(d.created_at, 'YYYY.mm.dd') as date
				FROM dream d
					INNER JOIN identity_user iu on d.user_id = iu.id
				LIMIT @pageSize OFFSET @page
				";
			const int pageSize = 10;
			var sqlParam = new
			{
				pageSize,
				page = (request.Page - 1) * pageSize
			};
			
			return _unitOfWork.Connection.QueryAsync<DreamDto>(sql, sqlParam, _unitOfWork.Transaction);
		}
	}
}