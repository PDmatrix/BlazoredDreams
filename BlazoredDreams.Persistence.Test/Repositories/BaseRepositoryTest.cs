using System.Threading.Tasks;
using Dapper;
using Xunit;

namespace BlazoredDreams.Persistence.Test.Repositories
{
	
	[Collection("Database collection")]
	public class BaseRepositoryTest
	{
		protected DatabaseFixture DatabaseFixture { get; }

		protected BaseRepositoryTest(DatabaseFixture databaseFixture)
		{
			DatabaseFixture = databaseFixture;
		}

		/// <summary>
		/// Truncates table and its sequence asynchronously
		/// </summary>
		/// <param name="tableName"></param>
		/// <returns></returns>
		protected async Task TruncateTableAsync(string tableName)
		{
			var sql =
				$@"TRUNCATE {tableName} RESTART IDENTITY CASCADE";
			await DatabaseFixture.UnitOfWork.Connection.ExecuteAsync(sql);
			DatabaseFixture.UnitOfWork.Commit();
		}

		/// <summary>
		/// Truncates table and resets its sequence
		/// </summary>
		/// <param name="tableName"></param>
		protected void TruncateTable(string tableName)
		{
			var sql =
				$@"TRUNCATE {tableName} RESTART IDENTITY CASCADE";
			DatabaseFixture.UnitOfWork.Connection.Execute(sql);
			DatabaseFixture.UnitOfWork.Commit();
		}
	}
}