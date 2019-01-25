using Xunit;

namespace BlazoredDreams.Persistence.Test
{
	[CollectionDefinition("Database collection")]	
	public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
	{
		
	}
}