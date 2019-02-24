using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Xunit;

namespace BlazoredDreams.Persistence.Test.Repositories
{
	public class DreamTest : BaseRepositoryTest, IAsyncLifetime
	{
		public DreamTest(DatabaseFixture databaseFixture) : base(databaseFixture)
		{
		}

		private async Task InitSqlAsync()
		{
			const string sql =
				@"INSERT INTO identity_user (id, identifier) VALUES (1, 'user');
				  INSERT INTO dream (content, user_id) VALUES ('foo', 1), ('bar', 1);";
			await DatabaseFixture.UnitOfWork.Connection.ExecuteAsync(sql);
			DatabaseFixture.UnitOfWork.Commit();
		}

		[Fact]
		public async Task SelectById()
		{
			// Arrange
			await InitSqlAsync();
			// Act
			var dream = await DatabaseFixture.UnitOfWork.DreamRepository.GetAsync(1);
			// Assert
			Assert.Equal("foo", dream.Content);
		}

		[Fact]
		public async Task SelectEverything()
		{
			// Arrange
			await InitSqlAsync();
			// Act
			var all = (await DatabaseFixture.UnitOfWork.DreamRepository.GetAllAsync()).ToList();
			// Assert
			Assert.True(all.Count == 2);
			Assert.Equal("foo", all[0].Content);
			Assert.Equal("bar", all[1].Content);
		}

		[Fact]
		public async Task SelectOffset()
		{
			// Arrange
			await InitSqlAsync();
			// Act
			var all = (await DatabaseFixture.UnitOfWork.DreamRepository.GetAllAsync(1, 1)).ToList();
			// Assert
			Assert.Single(all);
			Assert.Equal("foo", all[0].Content);
		}
		
		[Fact]
		public async Task InsertOne()
		{
			// Arrange
			await InitSqlAsync();
			var dream = new Domain.Entities.Dream {Content = "baz", UserId = 1}; 
			// Act
			await DatabaseFixture.UnitOfWork.DreamRepository.InsertAsync(dream);
			DatabaseFixture.UnitOfWork.Commit();
			var selectedDream = await DatabaseFixture.UnitOfWork.DreamRepository.GetAsync(3);
			// Assert
			Assert.Equal(dream.Content, selectedDream.Content);
			Assert.Equal(dream.UserId, selectedDream.UserId);
		}

		[Fact]
		public async Task UpdateOne()
		{
			// Arrange
			await InitSqlAsync();
			var dream = new Domain.Entities.Dream {Id = 1, Content = "baz", UserId = 1}; 
			// Act
			await DatabaseFixture.UnitOfWork.DreamRepository.UpdateAsync(dream);
			DatabaseFixture.UnitOfWork.Commit();
			var updatedDream = await DatabaseFixture.UnitOfWork.DreamRepository.GetAsync(1);
			var notUpdatedDream = await DatabaseFixture.UnitOfWork.DreamRepository.GetAsync(2);
			// Assert
			Assert.Equal("baz", updatedDream.Content);
			Assert.Equal("bar", notUpdatedDream.Content);
		}

		[Fact]
		public async Task DeleteOne()
		{
			// Arrange
			await InitSqlAsync();
			// Act
			await DatabaseFixture.UnitOfWork.DreamRepository.DeleteAsync(1);
			DatabaseFixture.UnitOfWork.Commit();
			var all = await DatabaseFixture.UnitOfWork.DreamRepository.GetAllAsync();
			// Assert
			Assert.Single(all);
		}

		public async Task InitializeAsync()
		{
			await TruncateTableAsync("identity_user");
			await TruncateTableAsync("dream").ConfigureAwait(false);
		}

		public Task DisposeAsync() => Task.CompletedTask;
	}
}