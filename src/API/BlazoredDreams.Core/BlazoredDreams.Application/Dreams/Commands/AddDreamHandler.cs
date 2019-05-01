using System;
using System.Threading;
using System.Threading.Tasks;
using BlazoredDreams.Application.Interfaces;
using Dapper;
using MediatR;

namespace BlazoredDreams.Application.Dreams.Commands
{
	public class AddDreamCommand : IRequest<int>
	{
		public string Content { get; set; }
		public string UserId { get; set; }
		public DateTime Date { get; set; }
	}
	
	// ReSharper disable once UnusedMember.Global
	public class AddDreamHandler : IRequestHandler<AddDreamCommand, int>
	{
		private readonly IUnitOfWork _unitOfWork;
		
		public AddDreamHandler(IUnitOfWorkFactory unitOfWorkFactory)
		{
			_unitOfWork = unitOfWorkFactory.Create();
		}
		
		public async Task<int> Handle(AddDreamCommand request, CancellationToken cancellationToken)
		{
			const string sql =
				@"
				INSERT INTO dream (content, user_id, date)
					VALUES (@content, @userId, @date)
				RETURNING id
				";
			var postId = await _unitOfWork.Connection.QuerySingleOrDefaultAsync<int>(sql, request, _unitOfWork.Transaction);
			return postId;
		}
	}
}