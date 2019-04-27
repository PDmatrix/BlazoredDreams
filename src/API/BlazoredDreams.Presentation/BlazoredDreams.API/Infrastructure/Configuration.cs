using System;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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

		public static void AddCustomSwagger(this IServiceCollection services)
		{
			services.AddSwaggerDocument(options =>
			{
				options.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT"));
				options.DocumentProcessors.Add(new SecurityDefinitionAppender("JWT", new SwaggerSecurityScheme
				{
					Type = SwaggerSecuritySchemeType.ApiKey,
					Name = "Authorization",
					In = SwaggerSecurityApiKeyLocation.Header,
					Description = "Type into the textbox: Bearer {your JWT token}."
				}));
			});
		}

		public static void AddCustomAuthentication(this IServiceCollection services)
		{
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.Authority = "https://dtxauth.auth0.com/";
				options.Audience = "http://localhost:5000/api";
				options.RequireHttpsMetadata = false;
			});
		}
	}
}