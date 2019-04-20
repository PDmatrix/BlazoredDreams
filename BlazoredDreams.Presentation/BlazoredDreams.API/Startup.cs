using BlazoredDreams.Application.Posts.Queries;
using BlazoredDreams.Persistence;
using Blog.API.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace BlazoredDreams.API
{
	public class Startup
	{
		private IConfiguration Configuration { get; }
		private IHostingEnvironment Environment { get; }
		
		public Startup(IConfiguration configuration, IHostingEnvironment environment)
		{
			Configuration = configuration;
			Environment = environment;
		}
		
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvcCore()
				.AddJsonFormatters()
				.AddCors()
				.AddJsonOptions(x =>
				{
					x.SerializerSettings.ContractResolver = new DefaultContractResolver
					{
						NamingStrategy = new SnakeCaseNamingStrategy()
					};
				})
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
			
			services.AddOpenApiDocument();
			services.AddApiVersioning(options =>
			{
				options.AssumeDefaultVersionWhenUnspecified = true;
				options.ApiVersionReader = new UrlSegmentApiVersionReader();
				options.DefaultApiVersion = new ApiVersion(1, 0);
			});
			services.AddVersionedApiExplorer(options => { options.GroupNameFormat = "VV"; });
			services.AddMediatR(typeof(GetAllPostsHandler));
			services.AddScoped<IUnitOfWorkFactory>(provider => 
				new UnitOfWorkFactory(Configuration.GetConnectionString("DefaultConnection")));
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			
			app.UseCors(options => options.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());
			
			app.UseMvc(routes =>
			{
				routes.MapRoute(
					"DefaultApi",
					"api/{controller}/{action}");
			});
			
			app.UseSwagger();
			app.UseSwaggerUi3();
		}
	}
}