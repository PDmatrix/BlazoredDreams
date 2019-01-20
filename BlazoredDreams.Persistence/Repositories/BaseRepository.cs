using System.Data;

namespace BlazoredDreams.Persistence.Repositories
{
	public class BaseRepository
	{
		protected IDbTransaction Transaction { get; }
        protected IDbConnection Connection => Transaction.Connection;

        public BaseRepository(IDbTransaction transaction)
        {
            Transaction = transaction;
        }
	}
}