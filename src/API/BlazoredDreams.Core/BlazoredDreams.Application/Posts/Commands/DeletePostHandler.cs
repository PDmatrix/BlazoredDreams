using System.Threading;
using System.Threading.Tasks;
using BlazoredDreams.Application.Interfaces;
using Dapper;
using MediatR;

namespace BlazoredDreams.Application.Posts.Commands
{
	public class DeletePostCommand : IRequest
	{
		public int Id { get; set; }
	}
	
	// ReSharper disable once UnusedMember.Global
	public class DeletePostHandler : AsyncRequestHandler<DeletePostCommand>
	{
		private readonly IUnitOfWork _unitOfWork;
		
		public DeletePostHandler(IUnitOfWorkFactory unitOfWorkFactory)
		{
			_unitOfWork = unitOfWorkFactory.Create();
		}

		protected override async Task Handle(DeletePostCommand request, CancellationToken cancellationToken)
		{
			const string sql =
				@"
				DELETE FROM post
				WHERE id = @id
				";
			await _unitOfWork.Connection.ExecuteAsync(sql, new {request.Id}, _unitOfWork.Transaction);
		}
	}
}