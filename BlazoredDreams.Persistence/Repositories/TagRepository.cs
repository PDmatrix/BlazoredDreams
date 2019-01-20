using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using BlazoredDreams.Application.Interfaces.DataAccess;
using BlazoredDreams.Domain.Entities;
using Dapper;
using Dapper.Contrib.Extensions;

namespace BlazoredDreams.Persistence.Repositories
{
	public class TagRepository : BaseRepository, ITagRepository
	{
		public TagRepository(IDbTransaction transaction) : base(transaction)
		{
		}

		public async Task RemoveAsync(int id, CancellationToken ct = default)
		{
			await Connection.DeleteAsync(new Tag {Id = id});
		}

		public async Task AddAsync(Tag entity, CancellationToken ct = default)
		{
			await Connection.InsertAsync(entity, Transaction);
		}

		public async Task UpdateAsync(Tag entity, CancellationToken ct = default)
		{
			await Connection.UpdateAsync(entity, Transaction);
		}

		public async Task<Tag> GetAsync(int id, CancellationToken ct = default)
		{
			return await Connection.GetAsync<Tag>(id, Transaction);
		}

		public async Task<Tag> GetAsync(Tag entity, CancellationToken ct = default)
		{
			return await GetAsync(entity.Id, ct);
		}

		public async Task<IEnumerable<Tag>> GetAsync(CancellationToken ct = default)
		{
			return await Connection.GetAllAsync<Tag>(Transaction);
		}
	}
}