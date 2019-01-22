using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using BlazoredDreams.Application.Interfaces.DataAccess;
using BlazoredDreams.Domain.Entities;

namespace BlazoredDreams.Persistence.Repositories
{
	public class DreamRepository : BaseRepository, IDreamRepository
	{
		public DreamRepository(IDbTransaction transaction) : base(transaction)
		{
		}
		
		public Task RemoveAsync(int id, CancellationToken ct = default)
		{
			throw new System.NotImplementedException();
		}

		public Task AddAsync(Dream entity, CancellationToken ct = default)
		{
			throw new System.NotImplementedException();
		}

		public Task UpdateAsync(Dream entity, CancellationToken ct = default)
		{
			throw new System.NotImplementedException();
		}

		public Task<Dream> GetAsync(int id, CancellationToken ct = default)
		{
			throw new System.NotImplementedException();
		}

		public Task<IEnumerable<Dream>> GetAsync(CancellationToken ct = default)
		{
			throw new System.NotImplementedException();
		}
	}
}