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
		
		public async Task DeleteAsync(int id, CancellationToken ct = default)
		{
			await Connection.ExecuteAsync(
				@"DELETE FROM dream WHERE id = @id", new {id}, Transaction);
		}

		public async Task InsertAsync(Dream entity, CancellationToken ct = default)
		{
			await Connection.ExecuteAsync(
				@"INSERT INTO dream (content, user_id) values (@content, @userId)", entity, Transaction);
		}

		public async Task UpdateAsync(Dream entity, CancellationToken ct = default)
		{
			await Connection.ExecuteAsync(
				@"UPDATE dream SET content = @content, user_id = @userId WHERE id = @id", entity, Transaction);
		}

		public async Task<Dream> GetAsync(int id, CancellationToken ct = default)
		{
			return await Connection.QuerySingleOrDefaultAsync<Dream>(
				@"SELECT * FROM dream WHERE id = @id", new {id}, Transaction);
		}

		public async Task<IEnumerable<Dream>> GetAsync(CancellationToken ct = default)
		{
			return await Connection.QueryAsync<Dream>(
				@"SELECT * FROM dream", transaction: Transaction);
		}
	}
}