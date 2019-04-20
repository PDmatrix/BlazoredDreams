using BlazoredDreams.Application.Interfaces;
using BlazoredDreams.Application.Posts.Queries;
using BlazoredDreams.API.Infrastructure;
using BlazoredDreams.Persistence;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]
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
			services.AddCustomMvc(Environment);
			services.AddOpenApiDocument();
			services.AddCustomApiVersioning();
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