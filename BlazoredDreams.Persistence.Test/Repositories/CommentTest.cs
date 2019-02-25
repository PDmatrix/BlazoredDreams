using System.Linq;
using System.Threading.Tasks;
using Dapper;
using FluentAssertions;
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
				  INSERT INTO post (title, user_id, dream_id, excerpt) VALUES ('foo', 1, 1, 'foo');
				  INSERT INTO comment (content, post_id, user_id) 
				  VALUES ('foo', 1, 1), ('bar', 1, 1)";
			await DatabaseFixture.UnitOfWork.Connection.ExecuteAsync(sql);
			DatabaseFixture.UnitOfWork.Commit();
		}

		[Fact]
		public async Task SelectById()
		{
			// Arrange
			await InitSqlAsync();
			// Act
			var comment = await DatabaseFixture.UnitOfWork.CommentRepository.GetAsync(1);
			// Assert
			comment.Content.Should().Be("foo");
		}

		[Fact]
		public async Task SelectEverything()
		{
			// Arrange
			await InitSqlAsync();
			// Act
			var all = (await DatabaseFixture.UnitOfWork.CommentRepository.GetAllAsync()).ToList();
			// Assert
			all.Should().HaveCount(2);
			all.First().Content.Should().Be("foo");
			all.Last().Content.Should().Be("bar");
		}
		
		[Fact]
		public async Task SelectOffset()
		{
			// Arrange
			await InitSqlAsync();
			// Act
			var all = (await DatabaseFixture.UnitOfWork.CommentRepository.GetAllAsync(1, 1)).ToList();
			// Assert
			all.Should().HaveCount(1);
			all.First().Content.Should().Be("foo");
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
			selectedComment.Should().BeEquivalentTo(comment, options =>
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
			var comment = new Domain.Entities.Comment {Id = 1, Content = "baz", UserId = 1, PostId = 1}; 
			// Act
			await DatabaseFixture.UnitOfWork.CommentRepository.UpdateAsync(comment);
			DatabaseFixture.UnitOfWork.Commit();
			var updatedComment = await DatabaseFixture.UnitOfWork.CommentRepository.GetAsync(1);
			var notUpdatedComment = await DatabaseFixture.UnitOfWork.CommentRepository.GetAsync(2);
			// Assert
			updatedComment.Content.Should().Be("baz");
			notUpdatedComment.Content.Should().Be("bar");
		}

		[Fact]
		public async Task DeleteOne()
		{
			// Arrange
			await InitSqlAsync();
			// Act
			await DatabaseFixture.UnitOfWork.CommentRepository.DeleteAsync(1);
			DatabaseFixture.UnitOfWork.Commit();
			var all = await DatabaseFixture.UnitOfWork.CommentRepository.GetAllAsync();
			// Assert
			all.Should().HaveCount(1);
		}

		public async Task InitializeAsync()
		{
			await TruncateTableAsync("identity_user");
			await TruncateTableAsync("dream");
			await TruncateTableAsync("post");
			await TruncateTableAsync("comment").ConfigureAwait(false);
		}

		public Task DisposeAsync() => Task.CompletedTask;
	}
}