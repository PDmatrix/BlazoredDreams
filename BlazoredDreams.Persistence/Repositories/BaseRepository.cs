using System.Data;

namespace BlazoredDreams.Persistence.Repositories
{
	public class BaseRepository
	{
		protected IDbTransaction Transaction { get; }
        protected IDbConnection Connection => Transaction.Connection;

        protected BaseRepository(IDbTransaction transaction)
        {
            Transaction = transaction;
        }
	}
}