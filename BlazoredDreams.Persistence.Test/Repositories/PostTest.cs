using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Xunit;

namespace BlazoredDreams.Persistence.Test.Repositories
{
	public class PostTest : BaseRepositoryTest, IAsyncLifetime
	{
		public PostTest(DatabaseFixture databaseFixture) : base(databaseFixture)
		{
		}

		private async Task InitSqlAsync()
		{
			const string sql =
				@"INSERT INTO identity_user (id, identifier) VALUES (1, 'foo');
				  INSERT INTO dream (content, user_id) VALUES ('foo', 1);
				  INSERT INTO post (title, dream_id, user_id) 
				  VALUES ('foo', 1, 1), ('bar', 1, 1);";
			await DatabaseFixture.UnitOfWork.Connection.ExecuteAsync(sql);
			DatabaseFixture.UnitOfWork.Commit();
		}
		
		[Fact]
		public async Task Exists()
		{
			// Arrange
			const string sql = @"SELECT to_regclass('public.post') IS NOT NULL AS exists;";
			// Act
			var queryData = await DatabaseFixture.UnitOfWork.Connection.QueryAsync(sql);
			var exists = queryData.FirstOrDefault()?.exists;
			// Assert
			Assert.NotNull(exists);
			Assert.True(exists);
		}

		[Fact]
		public async Task SelectById()
		{
			// Arrange
			await InitSqlAsync();
			// Act
			var post = await DatabaseFixture.UnitOfWork.PostRepository.GetAsync(1);
			// Assert
			Assert.Equal("foo", post.Title);
		}

		[Fact]
		public async Task SelectEverything()
		{
			// Arrange
			await InitSqlAsync();
			// Act
			var all = (await DatabaseFixture.UnitOfWork.PostRepository.GetAllAsync()).ToList();
			// Assert
			Assert.True(all.Count == 2);
			Assert.Equal("foo", all[0].Title);
			Assert.Equal("bar", all[1].Title);
		}

		[Fact]
		public async Task SelectOffset()
		{
			// Arrange
			await InitSqlAsync();
			// Act
			var all = (await DatabaseFixture.UnitOfWork.PostRepository.GetAllAsync(1, 1)).ToList();
			// Assert
			Assert.Single(all);
			Assert.Equal("foo", all[0].Title);
		}
		
		[Fact]
		public async Task InsertOne()
		{
			// Arrange
			await InitSqlAsync();
			var post = new Domain.Entities.Post {Title = "baz", UserId = 1, DreamId = 1}; 
			// Act
			await DatabaseFixture.UnitOfWork.PostRepository.InsertAsync(post);
			DatabaseFixture.UnitOfWork.Commit();
			var selectedPost = await DatabaseFixture.UnitOfWork.PostRepository.GetAsync(3);
			// Assert
			Assert.Equal(post.Title, selectedPost.Title);
			Assert.Equal(post.UserId, selectedPost.UserId);
			Assert.Equal(post.DreamId, selectedPost.DreamId);
		}

		[Fact]
		public async Task UpdateOne()
		{
			// Arrange
			await InitSqlAsync();
			var post = new Domain.Entities.Post {Id = 1, Title = "baz", UserId = 1, DreamId = 1}; 
			// Act
			await DatabaseFixture.UnitOfWork.PostRepository.UpdateAsync(post);
			DatabaseFixture.UnitOfWork.Commit();
			var updatedPost = await DatabaseFixture.UnitOfWork.PostRepository.GetAsync(1);
			var notUpdatedPost = await DatabaseFixture.UnitOfWork.PostRepository.GetAsync(2);
			// Assert
			Assert.Equal("baz", updatedPost.Title);
			Assert.Equal("bar", notUpdatedPost.Title);
		}

		[Fact]
		public async Task DeleteOne()
		{
			// Arrange
			await InitSqlAsync();
			// Act
			await DatabaseFixture.UnitOfWork.PostRepository.DeleteAsync(1);
			DatabaseFixture.UnitOfWork.Commit();
			var all = await DatabaseFixture.UnitOfWork.PostRepository.GetAllAsync();
			// Assert
			Assert.Single(all);
		}

		public async Task InitializeAsync()
		{
			await TruncateTableAsync("identity_user");
			await TruncateTableAsync("post");
			await TruncateTableAsync("dream").ConfigureAwait(false);
		}

		public Task DisposeAsync() => Task.CompletedTask;
	}
}