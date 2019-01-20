using System.Threading;
using System.Threading.Tasks;

namespace BlazoredDreams.Application.Interfaces.DataAccess
{
	public interface IUserLikesRepository
	{
		Task AddLikeAsync(int userId, CancellationToken ct = default);
		Task RemoveLikeAsync(int userId, CancellationToken ct = default);
	}
}