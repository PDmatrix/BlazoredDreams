using System.Data;
using System.Threading;
using System.Threading.Tasks;
using BlazoredDreams.Application.Interfaces.DataAccess;

namespace BlazoredDreams.Persistence.Repositories
{
	public class UserLikesRepository : BaseRepository, IUserLikesRepository
	{
		public UserLikesRepository(IDbTransaction transaction) : base(transaction)
		{
		}

		public Task AddLikeAsync(int userId, CancellationToken ct = default)
		{
			throw new System.NotImplementedException();
		}

		public Task RemoveLikeAsync(int userId, CancellationToken ct = default)
		{
			throw new System.NotImplementedException();
		}
	}
}