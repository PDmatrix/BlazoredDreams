using BlazoredDreams.App.Redux;
using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;
using BlazoredDreams.App.Services;
using BlazoredDreams.Application.Interfaces.DataAccess;
using BlazoredDreams.Persistence;
using BlazorRedux;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace BlazoredDreams.App
{
	public class Startup
	{
		
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<WeatherForecastService>();
			services.AddReduxStore<State, IAction>(new State(), Reducers.RootReducer, options =>
			{
				options.GetLocation = state => state.Location;
			});
		}

		public void Configure(IBlazorApplicationBuilder app)
		{
			app.AddComponent<App>("app");
		}
	}
}