using System.Linq;
using System.Threading.Tasks;
using Dapper;
using FluentAssertions;
using Xunit;

namespace BlazoredDreams.Persistence.Test.Repositories
{
	public class TagTest : BaseRepositoryTest, IAsyncLifetime
	{
		public TagTest(DatabaseFixture databaseFixture) : base(databaseFixture)
		{
		}

		private async Task InitSqlAsync()
		{
			const string sql = @"INSERT INTO tag (name) VALUES ('foo'), ('bar')";
			await DatabaseFixture.UnitOfWork.Connection.ExecuteAsync(sql);
			DatabaseFixture.UnitOfWork.Commit();
		}

		[Fact]
		public async Task SelectById()
		{
			// Arrange
			await InitSqlAsync();
			// Act
			var fooTag = await DatabaseFixture.UnitOfWork.TagRepository.GetAsync(1);
			var barTag = await DatabaseFixture.UnitOfWork.TagRepository.GetAsync(2);
			// Assert
			fooTag.Name.Should().Be("foo");
			barTag.Name.Should().Be("bar");
		}

		[Fact]
		public async Task SelectEverything()
		{
			// Arrange
			await InitSqlAsync();
			// Act
			var all = (await DatabaseFixture.UnitOfWork.TagRepository.GetAllAsync()).ToList();
			// Assert
			all.Should().HaveCount(2);
			all.First().Name.Should().Be("foo");
			all.Last().Name.Should().Be("bar");
		}

		[Fact]
		public async Task SelectOffset()
		{
			// Arrange
			await InitSqlAsync();
			// Act
			var all = (await DatabaseFixture.UnitOfWork.TagRepository.GetAllAsync(1, 1)).ToList();
			// Assert
			all.Should().HaveCount(1);
			all.First().Name.Should().Be("foo");
		}
		
		[Fact]
		public async Task InsertOne()
		{
			// Arrange
			var tag = new Domain.Entities.Tag {Id = 1, Name = "foo"}; 
			// Act
			await DatabaseFixture.UnitOfWork.TagRepository.InsertAsync(tag);
			DatabaseFixture.UnitOfWork.Commit();
			var selectedTag = await DatabaseFixture.UnitOfWork.TagRepository.GetAsync(1);
			// Assert
			selectedTag.Should().BeEquivalentTo(tag, options =>
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
			var tag = new Domain.Entities.Tag {Id = 1, Name = "baz"}; 
			// Act
			await DatabaseFixture.UnitOfWork.TagRepository.UpdateAsync(tag);
			DatabaseFixture.UnitOfWork.Commit();
			var selectedTag = await DatabaseFixture.UnitOfWork.TagRepository.GetAsync(1);
			var notUpdatedTag = await DatabaseFixture.UnitOfWork.TagRepository.GetAsync(2);
			// Assert
			selectedTag.Name.Should().Be("baz");
			notUpdatedTag.Name.Should().Be("bar");
		}

		[Fact]
		public async Task DeleteOne()
		{
			// Arrange
			await InitSqlAsync();
			// Act
			await DatabaseFixture.UnitOfWork.TagRepository.DeleteAsync(1);
			DatabaseFixture.UnitOfWork.Commit();
			var all = await DatabaseFixture.UnitOfWork.TagRepository.GetAllAsync();
			// Assert
			all.Should().HaveCount(1);
		}

		public Task InitializeAsync() => TruncateTableAsync("tag");

		public Task DisposeAsync() => Task.CompletedTask;
	}
}