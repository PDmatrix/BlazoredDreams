using System.Linq;
using System.Threading.Tasks;
using BlazoredDreams.Application.Posts.Models;
using Dapper;
using FluentAssertions;
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
				  INSERT INTO post (title, dream_id, user_id, excerpt) 
				  VALUES ('foo', 1, 1, 'foo'), ('bar', 1, 1, 'bar');";
			await DatabaseFixture.UnitOfWork.Connection.ExecuteAsync(sql);
			DatabaseFixture.UnitOfWork.Commit();
		}

		[Fact]
		public async Task SelectById()
		{
			// Arrange
			await InitSqlAsync();
			// Act
			var post = await DatabaseFixture.UnitOfWork.PostRepository.GetAsync(1);
			// Assert
			post.Title.Should().Be("foo");
			post.Excerpt.Should().Be("foo");
		}

		[Fact]
		public async Task SelectEverything()
		{
			// Arrange
			await InitSqlAsync();
			// Act
			var all = (await DatabaseFixture.UnitOfWork.PostRepository.GetAllAsync()).ToList();
			// Assert
			all.Should().HaveCount(2);
			all.First().Title.Should().Be("foo");
			all.First().Excerpt.Should().Be("foo");
			all.Last().Title.Should().Be("bar");
			all.Last().Excerpt.Should().Be("bar");
		}

		[Fact]
		public async Task SelectOffset()
		{
			// Arrange
			await InitSqlAsync();
			// Act
			var all = (await DatabaseFixture.UnitOfWork.PostRepository.GetAllAsync(1, 1)).ToList();
			// Assert
			all.Should().HaveCount(1);
			all.First().Title.Should().Be("foo");
			all.First().Excerpt.Should().Be("foo");
		}
		
		[Fact]
		public async Task InsertOne()
		{
			// Arrange
			await InitSqlAsync();
			var post = new Domain.Entities.Post {Title = "baz", UserId = 1, DreamId = 1, Excerpt = "baz"}; 
			// Act
			await DatabaseFixture.UnitOfWork.PostRepository.InsertAsync(post);
			DatabaseFixture.UnitOfWork.Commit();
			var selectedPost = await DatabaseFixture.UnitOfWork.PostRepository.GetAsync(3);
			// Assert
			selectedPost.Should().BeEquivalentTo(post, options =>
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
			var post = new Domain.Entities.Post {Id = 1, Title = "baz", UserId = 1, DreamId = 1, Excerpt = "baz"}; 
			// Act
			await DatabaseFixture.UnitOfWork.PostRepository.UpdateAsync(post);
			DatabaseFixture.UnitOfWork.Commit();
			var updatedPost = await DatabaseFixture.UnitOfWork.PostRepository.GetAsync(1);
			var notUpdatedPost = await DatabaseFixture.UnitOfWork.PostRepository.GetAsync(2);
			// Assert
			updatedPost.Title.Should().Be("baz");
			notUpdatedPost.Title.Should().Be("bar");
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
			all.Should().HaveCount(1);
		}

		[Fact]
		public async Task GetAllPostsDto()
		{
			// Arrange
			await InitSqlAsync();
			var expectedFirstPost = new GetAllPostDto
			{
				Tag = "Без тега",
				Comments = 0,
				Title = "foo",
				Excerpt = "foo",
				Username = "foo",
				TotalPages = 2
			};
			var expectedSecondPost = new GetAllPostDto
			{
				Tag = "Без тега",
				Comments = 0,
				Title = "bar",
				Excerpt = "bar",
				Username = "foo",
				TotalPages = 2
			};
			// Act
			var posts = await DatabaseFixture.UnitOfWork.PostRepository.GetAllPostsAsync();
			var getAllPostDtos = posts as GetAllPostDto[] ?? posts.ToArray();
			// Assert
			getAllPostDtos.Should().HaveCount(2);
			getAllPostDtos.First().Should().BeEquivalentTo(expectedFirstPost, options =>
				options.Excluding(r => r.Date));
			getAllPostDtos.Last().Should().BeEquivalentTo(expectedSecondPost, options =>
				options.Excluding(r => r.Date));
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