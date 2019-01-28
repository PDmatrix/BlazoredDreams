using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using BlazoredDreams.Application.Interfaces.DataAccess;
using BlazoredDreams.Domain.Entities;
using Dapper;

namespace BlazoredDreams.Persistence.Repositories
{
	public class UserRepository : BaseRepository, IUserRepository
	{
		public UserRepository(IDbTransaction transaction) : base(transaction)
		{
		}

		public async Task DeleteAsync(int id, CancellationToken ct = default)
		{
			await Connection.ExecuteAsync(
				@"DELETE FROM identity_user WHERE id = @id", new {id}, Transaction);
		}

		public async Task InsertAsync(IdentityUser entity, CancellationToken ct = default)
		{
			await Connection.ExecuteAsync(
				@"INSERT INTO identity_user (identifier) values (@identifier)", entity, Transaction);
		}

		public async Task UpdateAsync(IdentityUser entity, CancellationToken ct = default)
		{
			await Connection.ExecuteAsync(
				@"UPDATE identity_user SET identifier = @identifier WHERE id = @id", entity, Transaction);
		}

		public async Task<IdentityUser> GetAsync(int id, CancellationToken ct = default)
		{
			return await Connection.QuerySingleOrDefaultAsync<IdentityUser>(
				@"SELECT * FROM identity_user WHERE id = @id", new {id}, Transaction);
		}

		// TODO: Paging
		public async Task<IEnumerable<IdentityUser>> GetAsync(CancellationToken ct = default)
		{
			return await Connection.QueryAsync<IdentityUser>(
				@"SELECT * FROM identity_user", transaction: Transaction);
		}
	}
}