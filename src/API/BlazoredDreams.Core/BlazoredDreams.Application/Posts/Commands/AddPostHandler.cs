using System.Threading;
using System.Threading.Tasks;
using BlazoredDreams.Application.Interfaces;
using Dapper;
using MediatR;

namespace BlazoredDreams.Application.Posts.Commands
{
	public class AddPostCommand : IRequest<int>
	{
		public string Title { get; set; }
		public string Excerpt { get; set; }
		public int DreamId { get; set; }
		public string UserId { get; set; }
	}
	
	// ReSharper disable once UnusedMember.Global
	public class AddPostHandler : IRequestHandler<AddPostCommand, int>
	{
		private readonly IUnitOfWork _unitOfWork;
		
		public AddPostHandler(IUnitOfWorkFactory unitOfWorkFactory)
		{
			_unitOfWork = unitOfWorkFactory.Create();
		}
		
		public async Task<int> Handle(AddPostCommand request, CancellationToken cancellationToken)
		{
			const string sql =
				@"
				INSERT INTO post (title, user_id, dream_id, excerpt) 
					VALUES (@title, @userId, @dreamId, @excerpt)
				RETURNING id
				";
			var postId = await _unitOfWork.Connection.QuerySingleOrDefaultAsync<int>(sql, request, _unitOfWork.Transaction);
			return postId;
		}
	}
}