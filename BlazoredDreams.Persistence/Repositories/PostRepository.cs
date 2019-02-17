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

		public Task DeleteAsync(int id, CancellationToken ct = default)
        {
            return Connection.ExecuteAsync(
                @"DELETE FROM post WHERE id = @id", new {id}, Transaction);
        }

		public Task InsertAsync(Post entity, CancellationToken ct = default)
        {
            return Connection.ExecuteAsync(
                @"INSERT INTO post (title, user_id, dream_id, excerpt) values (@title, @userId, @dreamId, @excerpt)", entity, Transaction);
        }

		public Task UpdateAsync(Post entity, CancellationToken ct = default)
        {
            return Connection.ExecuteAsync(
                @"UPDATE post SET title = @title, user_id = @userId, dream_id = @dreamId, excerpt = @excerpt WHERE id = @id", entity, Transaction);
        }

		public Task<Post> GetAsync(int id, CancellationToken ct = default)
		{
			return Connection.QuerySingleOrDefaultAsync<Post>(
                @"SELECT * FROM post WHERE id = @id", new {id}, Transaction);
		}

		public Task<IEnumerable<Post>> GetAllAsync(int page = 1, int pageSize = 10, CancellationToken ct = default)
		{
			return Connection.QueryAsync<Post>(
                @"SELECT * FROM post LIMIT @pageSize OFFSET @page",
                new { pageSize, page = (page - 1) * pageSize }, Transaction);
        }
	}
}