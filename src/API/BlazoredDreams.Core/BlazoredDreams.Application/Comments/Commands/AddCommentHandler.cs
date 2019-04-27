using System.Threading;
using System.Threading.Tasks;
using BlazoredDreams.Application.Interfaces;
using Dapper;
using MediatR;

namespace BlazoredDreams.Application.Comments.Commands
{
	public class AddCommentCommand : IRequest<int>
	{
		public int PostId { get; set; }
		public string Content { get; set; }
		public string UserId { get; set; }
	}
	
	// ReSharper disable once UnusedMember.Global
	public class AddCommentHandler : IRequestHandler<AddCommentCommand, int>
	{
		private readonly IUnitOfWork _unitOfWork;
		
		public AddCommentHandler(IUnitOfWorkFactory unitOfWorkFactory)
		{
			_unitOfWork = unitOfWorkFactory.Create();
		}
		
		public Task<int> Handle(AddCommentCommand request, CancellationToken cancellationToken)
		{
			const string sql =
				@"
				INSERT INTO comment (content, post_id, user_id)
					VALUES (@content, @postId, @userId)
				RETURNING id
				";
			return _unitOfWork.Connection.QuerySingleOrDefaultAsync<int>(sql, request, _unitOfWork.Transaction);
		}
	}
}