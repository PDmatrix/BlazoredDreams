using Microsoft.AspNetCore.Blazor.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using BlazoredDreams.Application.Interfaces.DataAccess;
using BlazoredDreams.Persistence;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace BlazoredDreams.Server
{
	public class Startup
	{
		public IConfiguration Configuration { get; }
		
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}
		
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddServerSideBlazor<App.Startup>();

			services.AddResponseCompression(options =>
			{
				options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
				{
					MediaTypeNames.Application.Octet,
					WasmMediaTypeNames.Application.Wasm,
				});
			});
			services.AddMediatR(typeof(Application.Tags.Queries.GetAllTags)
				.GetTypeInfo().Assembly);
			services.AddScoped<IUnitOfWork>(s => 
				new UnitOfWork(Configuration.GetConnectionString("Source")));
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseResponseCompression();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseServerSideBlazor<App.Startup>();
		}
	}
}