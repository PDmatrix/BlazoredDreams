using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using BlazoredDreams.Application.Interfaces.DataAccess;
using BlazoredDreams.Application.Posts.Models;
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

		public Task<IEnumerable<GetAllPostDto>> GetAllPostsAsync(int page = 1, int pageSize = 10)
		{
			const string sql = @"
			SELECT
				iu.identifier as username,
				p.title,
				(SELECT COUNT(*) FROM comment c WHERE c.post_id = p.id) as comments,
				p.excerpt,
				to_char(p.created_at, 'YYYY.mm.dd') as date,
				COALESCE(t.name, 'Без тега') as tag,
				(SELECT COUNT(*) FROM post) as total_pages
			FROM post p
				INNER JOIN identity_user iu on p.user_id = iu.id
				INNER JOIN dream d on d.id = p.dream_id
				LEFT JOIN post_tags pt on p.id = pt.post_id
				LEFT JOIN tag t on pt.tag_id = t.id
			LIMIT @pageSize OFFSET @page
			";

			return Connection.QueryAsync<GetAllPostDto>(sql,
				new {pageSize, page = (page - 1) * pageSize}, Transaction);
		}
	}
}