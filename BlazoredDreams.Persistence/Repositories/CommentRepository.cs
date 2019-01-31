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

		public Task DeleteAsync(int id, CancellationToken ct = default)
        {
            return Connection.ExecuteAsync(
                @"DELETE FROM comment WHERE id = @id", new {id}, Transaction);
        }

		public Task InsertAsync(Comment entity, CancellationToken ct = default)
        {
            return Connection.ExecuteAsync(
                @"INSERT INTO comment (content, post_id, user_id) values (@content, @postId, @userId)", entity, Transaction);
        }

		public Task UpdateAsync(Comment entity, CancellationToken ct = default)
        {
            return Connection.ExecuteAsync(
                @"UPDATE comment SET content = @content, post_id = @postId, user_id = @userId WHERE id = @id", entity, Transaction);
        }

		public Task<Comment> GetAsync(int id, CancellationToken ct = default)
		{
			return Connection.QuerySingleOrDefaultAsync<Comment>(
                @"SELECT * FROM comment WHERE id = @id", new {id}, Transaction);
		}

		public Task<IEnumerable<Comment>> GetAllAsync(int page = 1, int pageSize = 10, CancellationToken ct = default)
		{
			return Connection.QueryAsync<Comment>(
                @"SELECT * FROM comment LIMIT @pageSize OFFSET @page",
                new { pageSize, page = (page - 1) * pageSize }, Transaction);
        }
	}
}