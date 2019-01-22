using System;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Xunit;

namespace BlazoredDreams.Persistence.Test.Repositories.Tag
{
	/*
	 * Check list:
	 * - Unit of work created
	 * - Table exists
	 * - Selecting
	 *   - By index
	 *   - Everything
	 * - Inserting
	 *   - One
	 * - Updating
	 *   - One
	 * - Deleting
	 *   - One
	 */

	public class Test : IClassFixture<TagFixture>, IDisposable
	{
		private readonly TagFixture _tagFixture;
		
		public Test(TagFixture tagFixture)
		{
			_tagFixture = tagFixture;
		}
		
		[Fact]
		public void Created()
		{
			Assert.NotNull(_tagFixture.UnitOfWork);
		}

		[Fact]
		public async Task Exists()
		{
			// Arrange
			const string sql = @"SELECT to_regclass('public.tag') IS NOT NULL AS exists;";
			// Act
			var queryData = await _tagFixture.UnitOfWork.Connection.QueryAsync(sql);
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
			await _tagFixture.UnitOfWork.Connection.ExecuteAsync(sql);
			_tagFixture.UnitOfWork.Commit();
			// Act
			var fooTag = await _tagFixture.UnitOfWork.TagRepository.GetAsync(1);
			var barTag = await _tagFixture.UnitOfWork.TagRepository.GetAsync(2);
			// Assert
			Assert.Equal("foo", fooTag.Name);
			Assert.Equal("bar", barTag.Name);
		}

		[Fact]
		public async Task SelectEverything()
		{
			// Arrange
			const string sql = @"INSERT INTO tag (id, name) VALUES (1, 'foo'), (2, 'bar')";
			await _tagFixture.UnitOfWork.Connection.ExecuteAsync(sql);
			_tagFixture.UnitOfWork.Commit();
			// Act
			var all = (await _tagFixture.UnitOfWork.TagRepository.GetAsync()).ToList();
			// Assert
			Assert.True(all.Count == 2);
			Assert.Equal("foo", all[0].Name);
			Assert.Equal("bar", all[1].Name);
		}
		

		[Fact]
		public async Task InsertOne()
		{
			// Arrange
			var tag = new Domain.Entities.Tag {Name = "foo"}; 
			// Act
			await _tagFixture.UnitOfWork.TagRepository.AddAsync(tag);
			_tagFixture.UnitOfWork.Commit();
			var selectedTag = await _tagFixture.UnitOfWork.TagRepository.GetAsync(1);
			// Assert
			Assert.Equal(tag.Name, selectedTag.Name);
		}

		[Fact]
		public async Task UpdateOne()
		{
			// Arrange
			const string sql = @"INSERT INTO tag (id, name) VALUES (1, 'foo')";
			await _tagFixture.UnitOfWork.Connection.ExecuteAsync(sql);
			_tagFixture.UnitOfWork.Commit();
			var tag = new Domain.Entities.Tag {Id = 1, Name = "bar"}; 
			// Act
			await _tagFixture.UnitOfWork.TagRepository.UpdateAsync(tag);
			_tagFixture.UnitOfWork.Commit();
			var selectedTag = await _tagFixture.UnitOfWork.TagRepository.GetAsync(1);
			// Assert
			Assert.Equal("bar", selectedTag.Name);
		}

		[Fact]
		public async Task DeleteOne()
		{
			// Arrange
			const string sql = @"INSERT INTO tag (id, name) VALUES (1, 'foo')";
			await _tagFixture.UnitOfWork.Connection.ExecuteAsync(sql);
			_tagFixture.UnitOfWork.Commit();
			// Act
			await _tagFixture.UnitOfWork.TagRepository.RemoveAsync(1);
			_tagFixture.UnitOfWork.Commit();
			var all = await _tagFixture.UnitOfWork.TagRepository.GetAsync();
			// Assert
			Assert.Equal(0, all.Count());
		}

		public void Dispose()
		{
			const string sql = "TRUNCATE tag";
			_tagFixture.UnitOfWork.Connection.Execute(sql);
			_tagFixture.UnitOfWork.Commit();
		}
	}
}