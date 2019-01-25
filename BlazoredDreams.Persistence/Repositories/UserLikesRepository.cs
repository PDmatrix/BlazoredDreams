using System.Data;
using System.Threading;
using System.Threading.Tasks;
using BlazoredDreams.Application.Interfaces.DataAccess;
using Dapper;

namespace BlazoredDreams.Persistence.Repositories
{
	public class UserLikesRepository : BaseRepository, IUserLikesRepository
	{
		public UserLikesRepository(IDbTransaction transaction) : base(transaction)
		{
		}

		public async Task AddLikeAsync(int userId, int postId, CancellationToken ct = default)
		{
			const string sql = 
				@"INSERT INTO user_likes (user_id, post_id) VALUES (@userId, @postId)";
			await Connection.ExecuteAsync(sql, new {userId, postId}, Transaction);
		}

		public async Task RemoveLikeAsync(int userId, int postId, CancellationToken ct = default)
		{
			const string sql = 
				@"DELETE FROM user_likes WHERE post_id = @postId AND user_id = @userId";
			await Connection.ExecuteAsync(sql, new {userId, postId}, Transaction);
		}
	}
}