using System;
using BlazoredDreams.Application.Interfaces.DataAccess;

namespace BlazoredDreams.Persistence.Test
{
	public class DatabaseFixture : IDisposable
	{
		public DatabaseFixture()
		{
			UnitOfWork = new UnitOfWork("Server=localhost;Port=5432;Database=blazoreddreams;User Id=blazoreddreams;Password=password");	
		}
		
		public IUnitOfWork UnitOfWork { get; }
		
		public void Dispose()
		{
			UnitOfWork?.Dispose();
		}
	}
}