using BlazoredDreams.App.Redux;
using BlazorRedux;
using MediatR;
using Microsoft.AspNetCore.Blazor.Components;

namespace BlazoredDreams.App.BaseComponents
{
	public class BaseComponent : ReduxComponent<State, IAction>
	{
		[Inject] public IMediator Mediator { get; set; }
	}
}