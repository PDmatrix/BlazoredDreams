using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Xunit;

namespace BlazoredDreams.Persistence.Test.Repositories
{
	public class UserTest : BaseRepositoryTest, IAsyncLifetime
	{
		public UserTest(DatabaseFixture databaseFixture) : base(databaseFixture)
		{
		}

		private async Task InitSqlAsync()
		{
			const string sql =
				@"INSERT INTO identity_user (identifier) VALUES ('foo'), ('bar');";
			await DatabaseFixture.UnitOfWork.Connection.ExecuteAsync(sql);
			DatabaseFixture.UnitOfWork.Commit();
		}

		[Fact]
		public async Task SelectById()
		{
			// Arrange
			await InitSqlAsync();
			// Act
			var user = await DatabaseFixture.UnitOfWork.UserRepository.GetAsync(1);
			// Assert
			Assert.Equal("foo", user.Identifier);
		}

		[Fact]
		public async Task SelectEverything()
		{
			// Arrange
			await InitSqlAsync();
			// Act
			var all = (await DatabaseFixture.UnitOfWork.UserRepository.GetAllAsync()).ToList();
			// Assert
			Assert.True(all.Count == 2);
			Assert.Equal("foo", all[0].Identifier);
			Assert.Equal("bar", all[1].Identifier);
		}

		[Fact]
		public async Task SelectOffset()
		{
			// Arrange
			await InitSqlAsync();
			// Act
			var all = (await DatabaseFixture.UnitOfWork.UserRepository.GetAllAsync(1, 1)).ToList();
			// Assert
			Assert.Single(all);
			Assert.Equal("foo", all[0].Identifier);
		}
		
		[Fact]
		public async Task InsertOne()
		{
			// Arrange
			await InitSqlAsync();
			var user = new Domain.Entities.IdentityUser {Identifier = "baz"}; 
			// Act
			await DatabaseFixture.UnitOfWork.UserRepository.InsertAsync(user);
			DatabaseFixture.UnitOfWork.Commit();
			var selectedUser = await DatabaseFixture.UnitOfWork.UserRepository.GetAsync(3);
			// Assert
			Assert.Equal(user.Identifier, selectedUser.Identifier);
		}

		[Fact]
		public async Task UpdateOne()
		{
			// Arrange
			await InitSqlAsync();
			var user = new Domain.Entities.IdentityUser {Id = 1, Identifier = "baz"}; 
			// Act
			await DatabaseFixture.UnitOfWork.UserRepository.UpdateAsync(user);
			DatabaseFixture.UnitOfWork.Commit();
			var updatedUser = await DatabaseFixture.UnitOfWork.UserRepository.GetAsync(1);
			var notUpdatedUser = await DatabaseFixture.UnitOfWork.UserRepository.GetAsync(2);
			// Assert
			Assert.Equal("baz", updatedUser.Identifier);
			Assert.Equal("bar", notUpdatedUser.Identifier);
		}

		[Fact]
		public async Task DeleteOne()
		{
			// Arrange
			await InitSqlAsync();
			// Act
			await DatabaseFixture.UnitOfWork.UserRepository.DeleteAsync(1);
			DatabaseFixture.UnitOfWork.Commit();
			var all = await DatabaseFixture.UnitOfWork.UserRepository.GetAllAsync();
			// Assert
			Assert.Single(all);
		}

		public Task InitializeAsync() => TruncateTableAsync("identity_user");

		public Task DisposeAsync() => Task.CompletedTask;
	}
}