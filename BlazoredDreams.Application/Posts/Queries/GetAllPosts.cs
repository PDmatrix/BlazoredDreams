using System.Collections.Generic;
using BlazoredDreams.Domain.Entities;
using MediatR;

namespace BlazoredDreams.Application.Posts.Queries
{
	public class GetAllPosts : IRequest<IEnumerable<Post>>
	{
		public int Page { get; set; } = 1;
	}
}