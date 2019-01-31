using System.Linq;
using System.Threading.Tasks;
using BlazoredDreams.Domain.Entities;
using Dapper;
using Xunit;

namespace BlazoredDreams.Persistence.Test.Repositories
{
	public class UserLikesTest : BaseRepositoryTest, IAsyncLifetime
	{
		public UserLikesTest(DatabaseFixture databaseFixture) : base(databaseFixture)
		{
		}

		[Fact]
		public async Task AddLike()
		{
			// Arrange
			await Init();
			// Act
			await DatabaseFixture.UnitOfWork.UserLikesRepository.AddLikeAsync(1, 1);
			const string selectSql = 
				@"SELECT * FROM user_likes WHERE user_id = 1 AND post_id = 1"; 
			var userLikes = 
				(await DatabaseFixture.UnitOfWork.Connection.QueryAsync<UserLikes>(selectSql)).FirstOrDefault();
			// Assert
			Assert.NotNull(userLikes);
			Assert.Equal(1, userLikes.PostId);
			Assert.Equal(1, userLikes.UserId);
		}

		[Fact]
		public async Task DeleteLike()
		{
			// Arrange
			await Init();
			const string sql =
				@"INSERT INTO user_likes VALUES (1, 1)";
			await DatabaseFixture.UnitOfWork.Connection.ExecuteAsync(sql);
			DatabaseFixture.UnitOfWork.Commit();
			// Act
			await DatabaseFixture.UnitOfWork.UserLikesRepository.RemoveLikeAsync(1, 1);
			const string selectSql = @"SELECT * FROM user_likes";
			var userLikes =
				await DatabaseFixture.UnitOfWork.Connection.QueryAsync<UserLikes>(selectSql);
			// Assert
			Assert.Empty(userLikes);
		}

		[Fact]
		public async Task IsLikedTest()
		{
			// Arrange
			await Init();
			const string sql =
				@"INSERT INTO user_likes VALUES (1, 1)";
			await DatabaseFixture.UnitOfWork.Connection.ExecuteAsync(sql);
			DatabaseFixture.UnitOfWork.Commit();
			// Act
			var liked = await DatabaseFixture.UnitOfWork.UserLikesRepository.IsLikedAsync(1, 1);
			var notLiked = await DatabaseFixture.UnitOfWork.UserLikesRepository.IsLikedAsync(1, 2);
			// Assert
			Assert.True(liked);
			Assert.False(notLiked);
		}

		private async Task Init()
		{
			const string sql =
				@"INSERT INTO identity_user (id, identifier) VALUES (1, 'user');
				  INSERT INTO dream (content, user_id) VALUES ('dream', 1);
				  INSERT INTO post (id, title, dream_id, user_id) VALUES (1, 'title', 1, 1);";
			await DatabaseFixture.UnitOfWork.Connection.ExecuteAsync(sql);
			DatabaseFixture.UnitOfWork.Commit();
		}

		public async Task InitializeAsync()
		{
			await TruncateTableAsync("identity_user");
			await TruncateTableAsync("user_likes");
			await TruncateTableAsync("post");
			await TruncateTableAsync("dream").ConfigureAwait(false);
		}

		public Task DisposeAsync() => Task.CompletedTask;
	}
}