using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BlazoredDreams.Domain.Entities;
using Blog.API.Application.Interfaces;
using MediatR;

namespace BlazoredDreams.Application.Tags.Queries
{
	public class GetAllTags : IRequest<IEnumerable<Tag>>
	{
		
	}
	
	// ReSharper disable once UnusedMember.Global
	public class GetAllTagsHandler : IRequestHandler<GetAllTags, IEnumerable<Tag>>
	{
		private readonly IUnitOfWork _unitOfWork;

		public GetAllTagsHandler(IUnitOfWorkFactory unitOfWorkFactory)
		{
			_unitOfWork = unitOfWorkFactory.Create();
		}
		
		public async Task<IEnumerable<Tag>> Handle(GetAllTags request, CancellationToken ct)
		{
			await Task.Delay(1);
			return new List<Tag>();
			//return _unitOfWork.TagRepository.GetAllAsync(cancellationToken: ct);
		}
	}
}