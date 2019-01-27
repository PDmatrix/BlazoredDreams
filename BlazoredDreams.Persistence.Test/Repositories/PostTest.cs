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
				@"INSERT INTO identity_user (id, identifier) VALUES (1, 'user');
				  INSERT INTO dream (content, user_id) VALUES ('dream', 1);
				  INSERT INTO post (title, dream_id, user_id) 
				  VALUES ('title', 1, 1), ('title2', 1, 1);";
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
			Assert.Equal("title", post.Title);
		}

		[Fact]
		public async Task SelectEverything()
		{
			// Arrange
			await InitSqlAsync();
			// Act
			var all = (await DatabaseFixture.UnitOfWork.PostRepository.GetAsync()).ToList();
			// Assert
			Assert.True(all.Count == 2);
			Assert.Equal("title", all[0].Title);
			Assert.Equal("title2", all[1].Title);
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
			var selectedPost = await DatabaseFixture.UnitOfWork.PostRepository.GetAsync(1);
			var notUpdatedPost = await DatabaseFixture.UnitOfWork.PostRepository.GetAsync(2);
			// Assert
			Assert.Equal("baz", selectedPost.Title);
			Assert.Equal("title2", notUpdatedPost.Title);
		}

		[Fact]
		public async Task DeleteOne()
		{
			// Arrange
			await InitSqlAsync();
			// Act
			await DatabaseFixture.UnitOfWork.PostRepository.DeleteAsync(1);
			DatabaseFixture.UnitOfWork.Commit();
			var all = await DatabaseFixture.UnitOfWork.PostRepository.GetAsync();
			// Assert
			Assert.Single(all);
		}

		public async Task InitializeAsync()
		{
			await TruncateTableAsync("identity_user");
			await TruncateTableAsync("post");
			await TruncateTableAsync("dream");
		}

		public Task DisposeAsync() => Task.CompletedTask;
	}
}