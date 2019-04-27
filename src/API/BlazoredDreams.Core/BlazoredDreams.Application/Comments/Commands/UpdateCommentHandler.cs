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
		public string Content { get; set; }
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
				UPDATE comment SET content = @content
				WHERE id = @id
				";
			await _unitOfWork.Connection.ExecuteAsync(sql, request, _unitOfWork.Transaction);
		}
	}
}