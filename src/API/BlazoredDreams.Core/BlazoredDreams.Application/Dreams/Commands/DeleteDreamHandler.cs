using System.Threading;
using System.Threading.Tasks;
using BlazoredDreams.Application.Interfaces;
using Dapper;
using MediatR;

namespace BlazoredDreams.Application.Dreams.Commands
{
	public class DeleteDreamCommand : IRequest
	{
		public int Id { get; set; }
	}
	
	// ReSharper disable once UnusedMember.Global
	public class DeleteDreamHandler : AsyncRequestHandler<DeleteDreamCommand>
	{
		private readonly IUnitOfWork _unitOfWork;
		
		public DeleteDreamHandler(IUnitOfWorkFactory unitOfWorkFactory)
		{
			_unitOfWork = unitOfWorkFactory.Create();
		}

		protected override async Task Handle(DeleteDreamCommand request, CancellationToken cancellationToken)
		{
			const string sql =
				@"
				DELETE FROM dream
				WHERE id = @id
				";
			await _unitOfWork.Connection.ExecuteAsync(sql, new {request.Id}, _unitOfWork.Transaction);
		}
	}
}