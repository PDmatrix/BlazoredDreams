using System.Threading;
using System.Threading.Tasks;
using BlazoredDreams.Application.Interfaces;
using Dapper;
using MediatR;

namespace BlazoredDreams.Application.Dreams.Commands
{
	public class UpdateDreamCommand : IRequest
	{
		public int Id { get; set; }
		public string Content { get; set; }
	}
	
	// ReSharper disable once UnusedMember.Global
	public class UpdateDreamHandler : AsyncRequestHandler<UpdateDreamCommand>
	{
		private readonly IUnitOfWork _unitOfWork;
		
		public UpdateDreamHandler(IUnitOfWorkFactory unitOfWorkFactory)
		{
			_unitOfWork = unitOfWorkFactory.Create();
		}

		protected override async Task Handle(UpdateDreamCommand request, CancellationToken cancellationToken)
		{
			const string sql =
				@"
				UPDATE dream SET content = @content
				WHERE id = @id
				";
			await _unitOfWork.Connection.ExecuteAsync(sql, request, _unitOfWork.Transaction);
		}
	}
}