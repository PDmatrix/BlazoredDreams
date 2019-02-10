using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;

namespace BlazoredDreams.App.BaseComponents
{
	public class BaseLayoutComponent : BaseComponent
	{
		[Parameter]
		protected RenderFragment Body { get; private set; }
	}
}