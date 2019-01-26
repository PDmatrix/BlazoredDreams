using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using BlazoredDreams.Application.Interfaces.DataAccess;
using BlazoredDreams.Domain.Entities;

namespace BlazoredDreams.Persistence.Repositories
{
	public class UserRepository : BaseRepository, IUserRepository
	{
		public UserRepository(IDbTransaction transaction) : base(transaction)
		{
		}

		public Task DeleteAsync(int id, CancellationToken ct = default)
		{
			throw new System.NotImplementedException();
		}

		public Task InsertAsync(IdentityUser entity, CancellationToken ct = default)
		{
			throw new System.NotImplementedException();
		}

		public Task UpdateAsync(IdentityUser entity, CancellationToken ct = default)
		{
			throw new System.NotImplementedException();
		}

		public Task<IdentityUser> GetAsync(int id, CancellationToken ct = default)
		{
			throw new System.NotImplementedException();
		}

		public Task<IEnumerable<IdentityUser>> GetAsync(CancellationToken ct = default)
		{
			throw new System.NotImplementedException();
		}
	}
}