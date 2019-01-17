using Microsoft.AspNetCore.Blazor.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Net.Mime;
using BlazoredDreams.Data;
using BlazoredDreams.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Serialization;

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
			services.AddMvcCore()
				.AddJsonFormatters()
				.SetCompatibilityVersion(CompatibilityVersion.Latest)
				.AddJsonOptions(x =>
				{
					x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
					x.SerializerSettings.ContractResolver = new DefaultContractResolver
					{
						NamingStrategy = new SnakeCaseNamingStrategy()
					};
				});
			services.AddServerSideBlazor<App.Startup>();

			services.AddResponseCompression(options =>
			{
				options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
				{
					MediaTypeNames.Application.Octet,
					WasmMediaTypeNames.Application.Wasm,
				});
			});

			services.AddScoped<IRepository<Tag>, TagRepository>();
			services.AddScoped<ITagService, TagService>();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseMvc(routes =>
			{
				routes.MapRoute(
					"DefaultApi",
					"api/{controller}/{action}");
			});
			app.UseResponseCompression();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseServerSideBlazor<App.Startup>();
		}
	}
}