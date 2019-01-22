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

		public async Task RemoveAsync(int id, CancellationToken ct = default)
		{
			await Connection.ExecuteAsync(
				@"DELETE FROM tag WHERE id = @id", new {id}, Transaction);
		}

		public async Task AddAsync(Tag entity, CancellationToken ct = default)
		{
			await Connection.ExecuteAsync(
				@"INSERT INTO tag (name) values (@name)", entity, Transaction);
		}

		public async Task UpdateAsync(Tag entity, CancellationToken ct = default)
		{
			await Connection.ExecuteAsync(
				@"UPDATE tag SET name = @name", entity, Transaction);
		}

		public async Task<Tag> GetAsync(int id, CancellationToken ct = default)
		{
			return await Connection.QuerySingleOrDefaultAsync<Tag>(
				@"SELECT * FROM tag WHERE id = @id", new { id }, Transaction);
		}

		public async Task<Tag> GetAsync(Tag entity, CancellationToken ct = default)
		{
			return await GetAsync(entity.Id, ct);
		}

		public async Task<IEnumerable<Tag>> GetAsync(CancellationToken ct = default)
		{
			return await Connection.QueryAsync<Tag>(
				@"SELECT * FROM tag", transaction: Transaction);
		}
	}
}