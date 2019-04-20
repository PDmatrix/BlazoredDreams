using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Respawn.Postgres;

namespace BlazoredDreams.API.IntegrationTest.Infrastructure
{
	public class TestFactory : WebApplicationFactory<Startup>
	{
		private IConfiguration Configuration { get; set; }
		private PostgresCheckpoint Checkpoint { get; }
		
		public TestFactory()
		{
			Checkpoint = new PostgresCheckpoint
			{
				SchemasToInclude = new[] {"public"},
				AutoCreateExtensions = true
			};
		}
		
		public Task ResetCheckpoint() => Checkpoint.Reset(Configuration.GetConnectionString("DefaultConnection"));
		
		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			builder.UseEnvironment("Testing");
			Configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.Testing.json", true, true)
				.Build();
			builder.UseConfiguration(Configuration);
		}
	}
}