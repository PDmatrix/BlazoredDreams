using System.IO;
using System.Threading;
using System.Threading.Tasks;
using BlazoredDreams.Application.Interfaces;
using CloudinaryDotNet;
using Dapper;
using MediatR;

namespace BlazoredDreams.Application.Posts.Commands
{
	public class AddImageToPostCommand : IRequest
	{
		public int Id { get; set; }
		public Stream FileStream { get; set; }
		public string FileName { get; set; }
	}
	
	// ReSharper disable once UnusedMember.Global
	public class AddImageToPostHandler : AsyncRequestHandler<AddImageToPostCommand>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ICloudinaryService _cloudinary;
		
		public AddImageToPostHandler(IUnitOfWorkFactory unitOfWorkFactory, ICloudinaryService cloudinary)
		{
			_cloudinary = cloudinary;
			_unitOfWork = unitOfWorkFactory.Create();
		}

		protected override async Task Handle(AddImageToPostCommand request, CancellationToken cancellationToken)
		{
			var imageUrl = _cloudinary.Upload(new FileDescription(request.FileName, request.FileStream));
			const string sql =
				@"
				UPDATE post SET cover = @imageUrl
				WHERE id = @id
				";
			await _unitOfWork.Connection.ExecuteAsync(sql, new {id = request.Id, imageUrl}, _unitOfWork.Transaction);
		}
	}
}