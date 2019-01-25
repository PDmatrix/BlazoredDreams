using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Xunit;

namespace BlazoredDreams.Persistence.Test.Repositories
{
	public class TagTest : BaseRepositoryTest, IAsyncLifetime
	{
		public TagTest(DatabaseFixture databaseFixture) : base(databaseFixture)
		{
		}
		
		[Fact]
		public async Task Exists()
		{
			// Arrange
			const string sql = @"SELECT to_regclass('public.tag') IS NOT NULL AS exists;";
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
			const string sql = @"INSERT INTO tag (id, name) VALUES (1, 'foo'), (2, 'bar')";
			await DatabaseFixture.UnitOfWork.Connection.ExecuteAsync(sql);
			DatabaseFixture.UnitOfWork.Commit();
			// Act
			var fooTag = await DatabaseFixture.UnitOfWork.TagRepository.GetAsync(1);
			var barTag = await DatabaseFixture.UnitOfWork.TagRepository.GetAsync(2);
			// Assert
			Assert.Equal("foo", fooTag.Name);
			Assert.Equal("bar", barTag.Name);
		}

		[Fact]
		public async Task SelectEverything()
		{
			// Arrange
			const string sql = @"INSERT INTO tag (id, name) VALUES (1, 'foo'), (2, 'bar')";
			await DatabaseFixture.UnitOfWork.Connection.ExecuteAsync(sql);
			DatabaseFixture.UnitOfWork.Commit();
			// Act
			var all = (await DatabaseFixture.UnitOfWork.TagRepository.GetAsync()).ToList();
			// Assert
			Assert.True(all.Count == 2);
			Assert.Equal("foo", all[0].Name);
			Assert.Equal("bar", all[1].Name);
		}

		[Fact]
		public async Task InsertOne()
		{
			// Arrange
			var tag = new Domain.Entities.Tag {Id = 1, Name = "foo"}; 
			// Act
			await DatabaseFixture.UnitOfWork.TagRepository.AddAsync(tag);
			DatabaseFixture.UnitOfWork.Commit();
			var selectedTag = await DatabaseFixture.UnitOfWork.TagRepository.GetAsync(1);
			// Assert
			Assert.Equal(tag.Name, selectedTag.Name);
		}

		[Fact]
		public async Task UpdateOne()
		{
			// Arrange
			const string sql = @"INSERT INTO tag (id, name) VALUES (1, 'foo')";
			await DatabaseFixture.UnitOfWork.Connection.ExecuteAsync(sql);
			DatabaseFixture.UnitOfWork.Commit();
			var tag = new Domain.Entities.Tag {Id = 1, Name = "bar"}; 
			// Act
			await DatabaseFixture.UnitOfWork.TagRepository.UpdateAsync(tag);
			DatabaseFixture.UnitOfWork.Commit();
			var selectedTag = await DatabaseFixture.UnitOfWork.TagRepository.GetAsync(1);
			// Assert
			Assert.Equal("bar", selectedTag.Name);
		}

		[Fact]
		public async Task DeleteOne()
		{
			// Arrange
			const string sql = @"INSERT INTO tag (id, name) VALUES (1, 'foo')";
			await DatabaseFixture.UnitOfWork.Connection.ExecuteAsync(sql);
			DatabaseFixture.UnitOfWork.Commit();
			// Act
			await DatabaseFixture.UnitOfWork.TagRepository.RemoveAsync(1);
			DatabaseFixture.UnitOfWork.Commit();
			var all = await DatabaseFixture.UnitOfWork.TagRepository.GetAsync();
			// Assert
			Assert.Empty(all);
		}
		
		public async Task InitializeAsync()
		{
			await TruncateTableAsync("tag");
		}

		public async Task DisposeAsync()
		{
			await TruncateTableAsync("tag");
		}
	}
}