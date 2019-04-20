using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace BlazoredDreams.API
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			IWebHostBuilder CreateWebHostBuilder() => 
				WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>();
			
			CreateWebHostBuilder().Build().Run();
		}
	}
}