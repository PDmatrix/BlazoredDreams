using System.Collections.Generic;

namespace BlazoredDreams.Data
{
	public interface IRepository<T>
	{
		IEnumerable<T> GetAll();
		T Get(long id);
		void Insert(T entity);
		void Update(T entity);
		void Delete(T entity);
	}	
}