using System;
using System.Threading.Tasks;
using BlazoredDreams.Application.Interfaces.DataAccess;
using Dapper;
using Xunit;

namespace BlazoredDreams.Persistence.Test.Repositories.Tag
{
	public class TagFixture : IAsyncLifetime
	{
		public TagFixture()
		{
			UnitOfWork = new UnitOfWork("Server=localhost;Port=5432;Database=test;User Id=test;Password=password");
		}

		public IUnitOfWork UnitOfWork { get; }
		
		public async Task InitializeAsync()
		{
			const string sql = @"create table if not exists tag
                    (
                        id serial not null
                            constraint pk_tag
                                primary key,
                        name text not null,
                        created_at timestamp default now() not null,
                        updated_at timestamp default now() not null
                    );";
			await UnitOfWork.Connection.ExecuteAsync(sql);
			UnitOfWork.Commit();
		}

		public async Task DisposeAsync()
		{
			const string sql = @"DROP TABLE tag";
            await UnitOfWork.Connection.ExecuteAsync(sql);
            UnitOfWork.Commit();
		}
	}
}