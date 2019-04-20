using System;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using NSwag;
using NSwag.SwaggerGeneration.Processors.Security;

namespace BlazoredDreams.API.Infrastructure
{
	public static class Configuration
	{
		public static void AddCustomMvc(this IServiceCollection services, IHostingEnvironment environment)
		{
			if (services == null)
				throw new ArgumentNullException(nameof (services));

			var builder = services.AddMvcCore(opt =>
			{
				if (environment.IsEnvironment("Testing"))
					opt.Filters.Add(typeof(AllowAnonymousFilter));
				opt.Filters.Add(typeof(TransactionFilter));
			});
			builder.AddJsonFormatters();
			builder.AddAuthorization();
			builder.AddCors();
			builder.AddFluentValidation(x =>
			{
				x.RegisterValidatorsFromAssemblyContaining<Startup>();
				x.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
			});

			builder.AddJsonOptions(x =>
			{
				x.SerializerSettings.ContractResolver = new DefaultContractResolver
				{
					NamingStrategy = new SnakeCaseNamingStrategy()
				};
			});
			builder.SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
		}

		public static void AddCustomApiVersioning(this IServiceCollection services)
		{
			if (services == null)
				throw new ArgumentNullException(nameof (services));
			
			services.AddApiVersioning(options =>
			{
				options.AssumeDefaultVersionWhenUnspecified = true;
				options.ApiVersionReader = new UrlSegmentApiVersionReader();
				options.DefaultApiVersion = new ApiVersion(1, 0);
			});
			services.AddVersionedApiExplorer(options => { options.GroupNameFormat = "VV"; });
		}

		public static void AddCustomAuthentication(this IServiceCollection services, string authority, string audience)
		{
			services.AddAuthentication("Bearer")
				.AddJwtBearer("Bearer", options =>
				{
					options.Authority = authority;
					options.RequireHttpsMetadata = false;

					options.Audience = audience;
				});
		}
	}
}