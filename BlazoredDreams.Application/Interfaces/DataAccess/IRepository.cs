using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BlazoredDreams.Application.Interfaces.DataAccess
{
	public interface IRepository<T> where T : class
	{
		Task DeleteAsync(int id, CancellationToken ct = default);
		Task InsertAsync(T entity, CancellationToken ct = default);
		Task UpdateAsync(T entity, CancellationToken ct = default);
		Task<T> GetAsync(int id, CancellationToken ct = default);
		Task<IEnumerable<T>> GetAsync(CancellationToken ct = default);
	}
}