using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazoredDreams.Application.Interfaces;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BlazoredDreams.API.Infrastructure
{
	public class TransactionFilter : IAsyncActionFilter
	{
		private readonly IUnitOfWorkFactory _unitOfWorkFactory;

		public TransactionFilter(IUnitOfWorkFactory unitOfWorkFactory)
		{
			_unitOfWorkFactory = unitOfWorkFactory;
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			using (var unitOfWork = _unitOfWorkFactory.Create())
			{
				var method = context.HttpContext.Request.Method;
				var safeAction = method == "GET" || IsActionTransactionFree(context.ActionDescriptor);
				if (safeAction)
				{
					await next();
					return;
				}
				
				await ProcessWithTransaction(next, unitOfWork);
			}
		}

		private static bool IsActionTransactionFree(ActionDescriptor actionDescriptor)
		{
			if (!(actionDescriptor is ControllerActionDescriptor controllerActionDescriptor))
				return false;
			
			var attributes =
				GetActionAttributes()
				.Union(GetControllerAttributes());

			return attributes.Any(r => r.GetType() == typeof(TransactionFreeAttribute));

			IEnumerable<object> GetActionAttributes() =>
				controllerActionDescriptor.MethodInfo.GetCustomAttributes(true);

			IEnumerable<object> GetControllerAttributes() =>
				controllerActionDescriptor.ControllerTypeInfo.GetCustomAttributes(true);
		}

		private static async Task ProcessWithTransaction(
			ActionExecutionDelegate next, IUnitOfWork unitOfWork)
		{
			try
			{
				unitOfWork.Begin();
				var actionExecuted = await next();
				if (actionExecuted.Exception != null && !actionExecuted.ExceptionHandled)
				{
					unitOfWork.Rollback();
				}
				else
				{
					unitOfWork.Commit();
				}
			}
			catch (Exception)
			{
				unitOfWork.Rollback();
				throw;
			}
		}
	}
}