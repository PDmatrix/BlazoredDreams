using System.Collections;
using System.Collections.Generic;
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
		public IEnumerable<string> Tags { get; set; }
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
			await ProcessTags(postId, request.Tags);
			return postId;
		}

		private async Task ProcessTags(int postId, IEnumerable<string> requestTags)
		{
			const string insertSql =
				@"
				INSERT INTO tag (name)
					VALUES (@name)
					ON CONFLICT DO NOTHING 
				RETURNING id
				";
			var insertedTagsId = new List<int>();
			foreach (var tag in requestTags)
			{
				var tagId = await _unitOfWork.Connection.QuerySingleOrDefaultAsync<int>(insertSql, new {Name = tag}, _unitOfWork.Transaction);
				insertedTagsId.Add(tagId);
			}

			const string updateSql =
				@"
				INSERT INTO post_tags (post_id, tag_id) 
					VALUES (@postId, @tagId)
				ON CONFLICT DO NOTHING
				";
			foreach (var tagId in insertedTagsId)
			{
				await _unitOfWork.Connection.ExecuteAsync(updateSql, new {TagId = tagId, PostId = postId}, _unitOfWork.Transaction);
			}
		}
	}
}