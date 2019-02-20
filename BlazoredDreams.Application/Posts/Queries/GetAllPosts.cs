using System.Collections.Generic;
using BlazoredDreams.Application.Posts.Models;
using MediatR;

namespace BlazoredDreams.Application.Posts.Queries
{
	public class GetAllPosts : IRequest<IEnumerable<GetAllPostDto>>
	{
		public int Page { get; set; } = 1;
	}
}