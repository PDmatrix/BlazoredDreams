using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using BlazoredDreams.Application.Interfaces.DataAccess;
using BlazoredDreams.Domain.Entities;
using Dapper;

namespace BlazoredDreams.Persistence.Repositories
{
	public class DreamRepository : BaseRepository, IDreamRepository
	{
		public DreamRepository(IDbTransaction transaction) : base(transaction)
		{
		}
		
		public Task DeleteAsync(int id, CancellationToken ct = default)
        {
            return Connection.ExecuteAsync(
                @"DELETE FROM dream WHERE id = @id", new {id}, Transaction);
        }

		public Task InsertAsync(Dream entity, CancellationToken ct = default)
        {
            return Connection.ExecuteAsync(
                @"INSERT INTO dream (content, user_id) values (@content, @userId)", entity, Transaction);
        }

		public Task UpdateAsync(Dream entity, CancellationToken ct = default)
        {
            return Connection.ExecuteAsync(
                @"UPDATE dream SET content = @content, user_id = @userId WHERE id = @id", entity, Transaction);
        }

		public Task<Dream> GetAsync(int id, CancellationToken ct = default)
		{
			return Connection.QuerySingleOrDefaultAsync<Dream>(
                @"SELECT * FROM dream WHERE id = @id", new {id}, Transaction);
		}

		public Task<IEnumerable<Dream>> GetAllAsync(int page = 1, int pageSize = 10, CancellationToken ct = default)
		{
			return Connection.QueryAsync<Dream>(
                @"SELECT * FROM dream LIMIT @pageSize OFFSET @page",
                new { pageSize, page = (page - 1) * pageSize }, Transaction);
        }
	}
}