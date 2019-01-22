using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Xunit;

namespace BlazoredDreams.Persistence.Test.Repositories.Tag
{
	public class Test : IClassFixture<TagFixture>
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
	}
}