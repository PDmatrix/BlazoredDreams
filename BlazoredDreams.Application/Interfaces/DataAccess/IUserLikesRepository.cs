using System.Threading;
using System.Threading.Tasks;

namespace BlazoredDreams.Application.Interfaces.DataAccess
{
	public interface IUserLikesRepository
	{
		Task AddLikeAsync(int userId, int postId, CancellationToken ct = default);
		Task RemoveLikeAsync(int userId, int postId, CancellationToken ct = default);
		Task<bool> IsLikedAsync(int userId, int postId, CancellationToken ct = default);
	}
}