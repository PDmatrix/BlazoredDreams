using System.Data;
using Blog.API.Application.Interfaces;

namespace BlazoredDreams.Persistence
{
	public sealed class UnitOfWork : IUnitOfWork
	{
		internal UnitOfWork(IDbConnection connection)
		{
			_connection = connection;
		}

		private readonly IDbConnection _connection;
		private IDbTransaction _transaction;

		IDbConnection IUnitOfWork.Connection => _connection;

		IDbTransaction IUnitOfWork.Transaction => _transaction;

		public void Begin()
		{
			_transaction = _connection.BeginTransaction();
		}

		public void Commit()
		{
			_transaction.Commit();
			DisposeTransaction();
		}

		public void Rollback()
		{
			_transaction?.Rollback();
			DisposeTransaction();
		}

		private void DisposeTransaction()
		{
			_transaction?.Dispose();
			_transaction = null;
		}

		public void Dispose()
		{
			_connection.Dispose();
		}
	}
}