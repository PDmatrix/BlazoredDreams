using System.Collections.Generic;
using BlazoredDreams.Domain.Entities;
using MediatR;

namespace BlazoredDreams.Application.Tags.Queries
{
	public class GetAllTags : IRequest<IEnumerable<Tag>>
	{
		
	}
}