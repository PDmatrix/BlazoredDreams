﻿using BlazoredDreams.Application.Interfaces;
using BlazoredDreams.Application.Posts.Queries;
using BlazoredDreams.API.Infrastructure;
using BlazoredDreams.Persistence;
using CloudinaryDotNet;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
			services.AddCustomSwagger();
			services.AddCustomAuthentication();
			services.AddCustomApiVersioning();
			services.AddMediatR(typeof(GetAllPostsHandler));
			services.AddHttpClient();
			services.AddScoped<IUnitOfWorkFactory>(provider => 
				new UnitOfWorkFactory(Configuration.GetConnectionString("DefaultConnection")));
			services.AddScoped<ICloudinaryService>(provider =>
				new CloudinaryService(Configuration.GetSection("Cloudinary").GetSection("Url").Value));
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			
			app.UseCors(options => options.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());
			app.UseAuthentication();
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