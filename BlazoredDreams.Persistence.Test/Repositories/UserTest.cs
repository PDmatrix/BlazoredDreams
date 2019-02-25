using System.Linq;
using System.Threading.Tasks;
using Dapper;
using FluentAssertions;
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
			user.Identifier.Should().Be("foo");
		}

		[Fact]
		public async Task SelectEverything()
		{
			// Arrange
			await InitSqlAsync();
			// Act
			var all = (await DatabaseFixture.UnitOfWork.UserRepository.GetAllAsync()).ToList();
			// Assert
			all.Should().HaveCount(2);
			all.First().Identifier.Should().Be("foo");
			all.Last().Identifier.Should().Be("bar");
		}

		[Fact]
		public async Task SelectOffset()
		{
			// Arrange
			await InitSqlAsync();
			// Act
			var all = (await DatabaseFixture.UnitOfWork.UserRepository.GetAllAsync(1, 1)).ToList();
			// Assert
			all.Should().HaveCount(1);
			all.First().Identifier.Should().Be("foo");
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
			selectedUser.Should().BeEquivalentTo(user, options =>
			{
				return options
					.Excluding(r => r.Id)
					.Excluding(r => r.CreatedAt)
					.Excluding(r => r.UpdatedAt);
			});
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
			updatedUser.Identifier.Should().Be("baz");
			notUpdatedUser.Identifier.Should().Be("bar");
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
			all.Should().HaveCount(1);
		}

		public Task InitializeAsync() => TruncateTableAsync("identity_user");

		public Task DisposeAsync() => Task.CompletedTask;
	}
}