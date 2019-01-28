using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using BlazoredDreams.Application.Interfaces.DataAccess;
using BlazoredDreams.Domain.Entities;
using Dapper;

namespace BlazoredDreams.Persistence.Repositories
{
	public class PostRepository : BaseRepository, IPostRepository
	{
		public PostRepository(IDbTransaction transaction) : base(transaction)
		{
		}

		public async Task DeleteAsync(int id, CancellationToken ct = default)
		{
			await Connection.ExecuteAsync(
				@"DELETE FROM post WHERE id = @id", new {id}, Transaction);
		}

		public async Task InsertAsync(Post entity, CancellationToken ct = default)
		{
			await Connection.ExecuteAsync(
				@"INSERT INTO post (title, user_id, dream_id) values (@title, @userId, @dreamId)", entity, Transaction);
		}

		public async Task UpdateAsync(Post entity, CancellationToken ct = default)
		{
			await Connection.ExecuteAsync(
				@"UPDATE post SET title = @title, user_id = @userId, dream_id = @dreamId WHERE id = @id", entity, Transaction);
		}

		public async Task<Post> GetAsync(int id, CancellationToken ct = default)
		{
			return await Connection.QuerySingleOrDefaultAsync<Post>(
				@"SELECT * FROM post WHERE id = @id", new {id}, Transaction);
		}

		// TODO: Paging
		public async Task<IEnumerable<Post>> GetAsync(CancellationToken ct = default)
		{
			return await Connection.QueryAsync<Post>(
				@"SELECT * FROM post", transaction: Transaction);
		}
	}
}