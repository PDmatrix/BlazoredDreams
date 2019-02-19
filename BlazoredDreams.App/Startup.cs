using BlazoredDreams.App.Redux;
using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;
using BlazoredDreams.App.Services;
using BlazorRedux;

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