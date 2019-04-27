using System.Threading;
using System.Threading.Tasks;
using BlazoredDreams.Application.Interfaces;
using Dapper;
using MediatR;

namespace BlazoredDreams.Application.Comments.Commands
{
	public class UpdateCommentCommand : IRequest
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Excerpt { get; set; }
	}
	
	// ReSharper disable once UnusedMember.Global
	public class UpdateCommentHandler : AsyncRequestHandler<UpdateCommentCommand>
	{
		private readonly IUnitOfWork _unitOfWork;
		
		public UpdateCommentHandler(IUnitOfWorkFactory unitOfWorkFactory)
		{
			_unitOfWork = unitOfWorkFactory.Create();
		}

		protected override async Task Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
		{
			const string sql =
				@"
				UPDATE post SET excerpt = @excerpt, title = @title
				WHERE id = @id
				";
			await _unitOfWork.Connection.ExecuteAsync(sql, request, _unitOfWork.Transaction);
		}
	}
}