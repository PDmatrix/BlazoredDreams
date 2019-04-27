using System.Threading;
using System.Threading.Tasks;
using BlazoredDreams.Application.Interfaces;
using Dapper;
using MediatR;

namespace BlazoredDreams.Application.Comments.Commands
{
	public class DeleteCommentCommand : IRequest
	{
		public int Id { get; set; }
	}
	
	// ReSharper disable once UnusedMember.Global
	public class DeleteCommentHandler : AsyncRequestHandler<DeleteCommentCommand>
	{
		private readonly IUnitOfWork _unitOfWork;
		
		public DeleteCommentHandler(IUnitOfWorkFactory unitOfWorkFactory)
		{
			_unitOfWork = unitOfWorkFactory.Create();
		}

		protected override async Task Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
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