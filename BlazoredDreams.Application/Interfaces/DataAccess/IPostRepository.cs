using System.Collections.Generic;
using System.Threading.Tasks;
using BlazoredDreams.Application.Posts.Models;
using BlazoredDreams.Domain.Entities;

namespace BlazoredDreams.Application.Interfaces.DataAccess
{
	public interface IPostRepository : IRepository<Post>
	{
		Task<IEnumerable<GetAllPostDto>> GetAllPostsAsync(int page = 1, int pageSize = 10);
	}
}