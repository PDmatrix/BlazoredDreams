using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using BlazoredDreams.Application.Interfaces.DataAccess;
using BlazoredDreams.Domain.Entities;
using Dapper;

namespace BlazoredDreams.Persistence.Repositories
{
	public class CommentRepository : BaseRepository, ICommentRepository
	{
		public CommentRepository(IDbTransaction transaction) : base(transaction)
		{
		}

		public async Task DeleteAsync(int id, CancellationToken ct = default)
		{
			await Connection.ExecuteAsync(
				@"DELETE FROM comment WHERE id = @id", new {id}, Transaction);
		}

		public async Task InsertAsync(Comment entity, CancellationToken ct = default)
		{
			await Connection.ExecuteAsync(
				@"INSERT INTO comment (content, post_id, user_id) values (@content, @postId, @userId)", entity, Transaction);
		}

		public async Task UpdateAsync(Comment entity, CancellationToken ct = default)
		{
			await Connection.ExecuteAsync(
				@"UPDATE comment SET content = @content, post_id = @postId, user_id = @userId WHERE id = @id", entity, Transaction);
		}

		public async Task<Comment> GetAsync(int id, CancellationToken ct = default)
		{
			return await Connection.QuerySingleOrDefaultAsync<Comment>(
				@"SELECT * FROM comment WHERE id = @id", new {id}, Transaction);
		}

		public async Task<IEnumerable<Comment>> GetAsync(CancellationToken ct = default)
		{
			return await Connection.QueryAsync<Comment>(
				@"SELECT * FROM comment", transaction: Transaction);
		}
	}
}