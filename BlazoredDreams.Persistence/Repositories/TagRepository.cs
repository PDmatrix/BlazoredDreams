using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using BlazoredDreams.Application.Interfaces.DataAccess;
using BlazoredDreams.Domain.Entities;
using Dapper;

namespace BlazoredDreams.Persistence.Repositories
{
	public class TagRepository : BaseRepository, ITagRepository
	{
		public TagRepository(IDbTransaction transaction) : base(transaction)
		{
		}

		public Task DeleteAsync(int id, CancellationToken ct = default)
        {
            return Connection.ExecuteAsync(
                @"DELETE FROM tag WHERE id = @id", new {id}, Transaction);
        }

		public Task InsertAsync(Tag entity, CancellationToken ct = default)
        {
            return Connection.ExecuteAsync(
                @"INSERT INTO tag (name) values (@name)", entity, Transaction);
        }

		public Task UpdateAsync(Tag entity, CancellationToken ct = default)
        {
            return Connection.ExecuteAsync(
                @"UPDATE tag SET name = @name WHERE id = @id", entity, Transaction);
        }

		public Task<Tag> GetAsync(int id, CancellationToken ct = default)
		{
			return Connection.QuerySingleOrDefaultAsync<Tag>(
                @"SELECT * FROM tag WHERE id = @id", new { id }, Transaction);
		}

		public Task<IEnumerable<Tag>> GetAllAsync(int page = 1, int pageSize = 10, CancellationToken ct = default)
        {
            return Connection.QueryAsync<Tag>(
                @"SELECT * FROM tag LIMIT @pageSize OFFSET @page",
                new { pageSize, page = (page - 1) * pageSize }, Transaction);

        }
	}
}