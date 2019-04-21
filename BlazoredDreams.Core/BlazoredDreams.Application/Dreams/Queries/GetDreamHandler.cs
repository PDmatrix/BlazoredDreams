using System.Threading;
using System.Threading.Tasks;
using BlazoredDreams.Application.Dreams.Models;
using BlazoredDreams.Application.Interfaces;
using Dapper;
using MediatR;

namespace BlazoredDreams.Application.Dreams.Queries
{
	public class GetDreamQuery : IRequest<DreamDto>
	{
		public int Id { get; set; }
	}
	
	// ReSharper disable once UnusedMember.Global
	public class GetDreamHandler : IRequestHandler<GetDreamQuery, DreamDto>
	{
		private readonly IUnitOfWork _unitOfWork;

		public GetDreamHandler(IUnitOfWorkFactory unitOfWorkFactory)
		{
			_unitOfWork = unitOfWorkFactory.Create();
		}
		
		public Task<DreamDto> Handle(GetDreamQuery request, CancellationToken ct)
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
				WHERE d.id = @id
				";

			return _unitOfWork.Connection.QuerySingleOrDefaultAsync<DreamDto>(sql, new {request.Id}, _unitOfWork.Transaction);
		}
	}
}