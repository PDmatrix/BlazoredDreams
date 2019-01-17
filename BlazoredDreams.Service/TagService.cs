using System;
using BlazoredDreams.Data;

namespace BlazoredDreams.Service
{
	public class TagService : ITagService
	{
		private readonly IRepository<Tag> _repository;
		public TagService(IRepository<Tag> repository)
		{
			_repository = repository;
		}

		public Tag Method(long id)
		{
			var value = _repository.Get(id);
			return value;
		}
	}
}