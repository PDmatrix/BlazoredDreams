using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BlazoredDreams.Application.Interfaces.DataAccess
{
	public interface IRepository<T> where T : class
	{
		Task DeleteAsync(int id, CancellationToken cancellationToken = default);
		Task InsertAsync(T entity, CancellationToken cancellationToken = default);
		Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
		Task<T> GetAsync(int id, CancellationToken cancellationToken = default);
		Task<IEnumerable<T>> GetAllAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default);
	}
}