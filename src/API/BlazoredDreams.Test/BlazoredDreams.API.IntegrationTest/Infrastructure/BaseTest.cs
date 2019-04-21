using System.Threading.Tasks;
using Xunit;

namespace BlazoredDreams.API.IntegrationTest.Infrastructure
{
	[Collection("WebApi collection")]
	public class BaseTest : IAsyncLifetime
	{
		private readonly TestFactory _factory;
		protected readonly HttpHandler HttpHandler;
		
		protected BaseTest(TestFactory factory)
		{
			_factory = factory;
			var client = _factory.CreateClient();
			HttpHandler = new HttpHandler(client);
		}
		
		public Task InitializeAsync() => Task.CompletedTask;

		public Task DisposeAsync() => _factory.ResetCheckpoint();
	}
}