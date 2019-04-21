using System;

namespace BlazoredDreams.API.Infrastructure
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class TransactionFreeAttribute : Attribute
	{
		
	}
}