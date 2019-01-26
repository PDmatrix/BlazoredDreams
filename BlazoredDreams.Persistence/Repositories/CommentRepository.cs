using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using BlazoredDreams.Application.Interfaces.DataAccess;
using BlazoredDreams.Domain.Entities;

namespace BlazoredDreams.Persistence.Repositories
{
	public class CommentRepository : BaseRepository, ICommentRepository
	{
		public CommentRepository(IDbTransaction transaction) : base(transaction)
		{
		}

		public Task DeleteAsync(int id, CancellationToken ct = default)
		{
			throw new System.NotImplementedException();
		}

		public Task InsertAsync(Comment entity, CancellationToken ct = default)
		{
			throw new System.NotImplementedException();
		}

		public Task UpdateAsync(Comment entity, CancellationToken ct = default)
		{
			throw new System.NotImplementedException();
		}

		public Task<Comment> GetAsync(int id, CancellationToken ct = default)
		{
			throw new System.NotImplementedException();
		}

		public Task<IEnumerable<Comment>> GetAsync(CancellationToken ct = default)
		{
			throw new System.NotImplementedException();
		}
	}
}