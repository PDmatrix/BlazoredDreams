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

		public Task DeleteAsync(int id, CancellationToken ct = default)
        {
            return Connection.ExecuteAsync(
                @"DELETE FROM identity_user WHERE id = @id", new {id}, Transaction);
        }

		public Task InsertAsync(IdentityUser entity, CancellationToken ct = default)
        {
            return Connection.ExecuteAsync(
                @"INSERT INTO identity_user (identifier) values (@identifier)", entity, Transaction);
        }

		public Task UpdateAsync(IdentityUser entity, CancellationToken ct = default)
        {
            return Connection.ExecuteAsync(
                @"UPDATE identity_user SET identifier = @identifier WHERE id = @id", entity, Transaction);
        }

		public Task<IdentityUser> GetAsync(int id, CancellationToken ct = default)
		{
			return Connection.QuerySingleOrDefaultAsync<IdentityUser>(
                @"SELECT * FROM identity_user WHERE id = @id", new {id}, Transaction);
		}

		public Task<IEnumerable<IdentityUser>> GetAllAsync(int page = 1, int pageSize = 10, CancellationToken ct = default)
        {
            return Connection.QueryAsync<IdentityUser>(
                @"SELECT * FROM identity_user LIMIT @pageSize OFFSET @page",
                new {pageSize, page = (page - 1) * pageSize}, Transaction);
        }
	}
}