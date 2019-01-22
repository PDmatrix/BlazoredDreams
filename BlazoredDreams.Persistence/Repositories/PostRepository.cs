using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using BlazoredDreams.Application.Interfaces.DataAccess;
using BlazoredDreams.Domain.Entities;

namespace BlazoredDreams.Persistence.Repositories
{
	public class PostRepository : BaseRepository, IPostRepository
	{
		public PostRepository(IDbTransaction transaction) : base(transaction)
		{
		}

		public Task RemoveAsync(int id, CancellationToken ct = default)
		{
			throw new System.NotImplementedException();
		}

		public Task AddAsync(Post entity, CancellationToken ct = default)
		{
			throw new System.NotImplementedException();
		}

		public Task UpdateAsync(Post entity, CancellationToken ct = default)
		{
			throw new System.NotImplementedException();
		}

		public Task<Post> GetAsync(int id, CancellationToken ct = default)
		{
			throw new System.NotImplementedException();
		}

		public Task<IEnumerable<Post>> GetAsync(CancellationToken ct = default)
		{
			throw new System.NotImplementedException();
		}
	}
}