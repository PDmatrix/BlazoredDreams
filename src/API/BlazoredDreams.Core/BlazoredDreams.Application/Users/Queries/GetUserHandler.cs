using System.Threading;
using System.Threading.Tasks;
using BlazoredDreams.Application.Interfaces;
using BlazoredDreams.Application.Posts.Models;
using BlazoredDreams.Application.Posts.Queries;
using BlazoredDreams.Application.Users.Models;
using Dapper;
using MediatR;

namespace BlazoredDreams.Application.Users.Queries
{
	public class GetUserQuery : IRequest<UserDto>
	{
		public string UserId { get; set; }
	}
	
	// ReSharper disable once UnusedMember.Global
	public class GetUserHandler : IRequestHandler<GetUserQuery, UserDto>
	{
		private readonly IUnitOfWork _unitOfWork;

		public GetUserHandler(IUnitOfWorkFactory unitOfWorkFactory)
		{
			_unitOfWork = unitOfWorkFactory.Create();
		}
		
		public Task<UserDto> Handle(GetUserQuery request, CancellationToken ct)
		{
			const string sql = 
				@"
				SELECT
					identifier as user_id,
					username,
				    email,
				    avatar
				FROM identity_user
				WHERE identifier = @userId
				";

			return _unitOfWork.Connection.QuerySingleOrDefaultAsync<UserDto>(sql, request, _unitOfWork.Transaction);
		}
	}
}