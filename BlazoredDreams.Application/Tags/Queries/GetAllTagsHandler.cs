using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlazoredDreams.Application.Interfaces.DataAccess;
using BlazoredDreams.Domain.Entities;
using MediatR;

namespace BlazoredDreams.Application.Tags.Queries
{
	// ReSharper disable once UnusedMember.Global
	public class GetAllTagsHandler : IRequestHandler<GetAllTags, IEnumerable<Tag>>
	{
		private readonly IUnitOfWork _unitOfWork;

		public GetAllTagsHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		
		public async Task<IEnumerable<Tag>> Handle(GetAllTags request, CancellationToken ct)
		{
			await _unitOfWork.TagRepository.AddAsync(new Tag
			{
				Name = "Custom"
			}, ct);
			return await _unitOfWork.TagRepository.GetAsync(ct);
		}
	}
}