using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Xunit;

namespace BlazoredDreams.Persistence.Test.Repositories
{
	public class CommentTest : BaseRepositoryTest, IAsyncLifetime
	{
		public CommentTest(DatabaseFixture databaseFixture) : base(databaseFixture)
		{
		}

		private async Task InitSqlAsync()
		{
			const string sql =
				@"INSERT INTO identity_user (id, identifier) VALUES (1, 'foo');
				  INSERT INTO dream (content, user_id) VALUES ('foo', 1);
				  INSERT INTO post (title, user_id, dream_id) VALUES ('foo', 1, 1);
				  INSERT INTO comment (content, post_id, user_id) 
				  VALUES ('foo', 1, 1), ('bar', 1, 1)";
			await DatabaseFixture.UnitOfWork.Connection.ExecuteAsync(sql);
			DatabaseFixture.UnitOfWork.Commit();
		}
		
		[Fact]
		public async Task Exists()
		{
			// Arrange
			const string sql = @"SELECT to_regclass('public.comment') IS NOT NULL AS exists;";
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
			var comment = await DatabaseFixture.UnitOfWork.CommentRepository.GetAsync(1);
			// Assert
			Assert.Equal("foo", comment.Content);
		}

		[Fact]
		public async Task SelectEverything()
		{
			// Arrange
			await InitSqlAsync();
			// Act
			var all = (await DatabaseFixture.UnitOfWork.CommentRepository.GetAsync()).ToList();
			// Assert
			Assert.True(all.Count == 2);
			Assert.Equal("foo", all[0].Content);
			Assert.Equal("bar", all[1].Content);
		}

		[Fact]
		public async Task InsertOne()
		{
			// Arrange
			await InitSqlAsync();
			var comment = new Domain.Entities.Comment {Content = "baz", UserId = 1, PostId = 1}; 
			// Act
			await DatabaseFixture.UnitOfWork.CommentRepository.InsertAsync(comment);
			DatabaseFixture.UnitOfWork.Commit();
			var selectedComment = await DatabaseFixture.UnitOfWork.CommentRepository.GetAsync(3);
			// Assert
			Assert.Equal(comment.Content, selectedComment.Content);
			Assert.Equal(comment.UserId, selectedComment.UserId);
			Assert.Equal(comment.PostId, selectedComment.PostId);
		}

		[Fact]
		public async Task UpdateOne()
		{
			// Arrange
			await InitSqlAsync();
			var comment = new Domain.Entities.Comment {Id = 1, Content = "baz", UserId = 1, PostId = 1}; 
			// Act
			await DatabaseFixture.UnitOfWork.CommentRepository.UpdateAsync(comment);
			DatabaseFixture.UnitOfWork.Commit();
			var updatedComment = await DatabaseFixture.UnitOfWork.CommentRepository.GetAsync(1);
			var notUpdatedComment = await DatabaseFixture.UnitOfWork.CommentRepository.GetAsync(2);
			// Assert
			Assert.Equal("baz", updatedComment.Content);
			Assert.Equal("bar", notUpdatedComment.Content);
		}

		[Fact]
		public async Task DeleteOne()
		{
			// Arrange
			await InitSqlAsync();
			// Act
			await DatabaseFixture.UnitOfWork.CommentRepository.DeleteAsync(1);
			DatabaseFixture.UnitOfWork.Commit();
			var all = await DatabaseFixture.UnitOfWork.CommentRepository.GetAsync();
			// Assert
			Assert.Single(all);
		}

		public async Task InitializeAsync()
		{
			await TruncateTableAsync("identity_user");
			await TruncateTableAsync("dream");
			await TruncateTableAsync("post");
			await TruncateTableAsync("comment");
		}

		public Task DisposeAsync() => Task.CompletedTask;
	}
}