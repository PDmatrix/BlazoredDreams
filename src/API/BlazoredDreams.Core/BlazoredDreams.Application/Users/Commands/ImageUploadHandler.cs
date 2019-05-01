using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using BlazoredDreams.Application.Interfaces;
using CloudinaryDotNet;
using Dapper;
using MediatR;

namespace BlazoredDreams.Application.Users.Commands
{
	public class ImageUploadCommand : IRequest
	{
		public string FileName { get; set; }
		public string UserId { get; set; }
		public Stream FileStream { get; set; }
	}
	
	// ReSharper disable once UnusedMember.Global
	public class ImageUploadHandler : AsyncRequestHandler<ImageUploadCommand>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ICloudinaryService _cloudinary;
		
		public ImageUploadHandler(IUnitOfWorkFactory unitOfWorkFactory, ICloudinaryService cloudinary)
		{
			_cloudinary = cloudinary;
			_unitOfWork = unitOfWorkFactory.Create();
		}
		
		protected override async Task Handle(ImageUploadCommand request, CancellationToken cancellationToken)
		{
			var avatar = _cloudinary.Upload(new FileDescription(request.FileName, request.FileStream));
			const string sql =
				@"
				UPDATE identity_user SET avatar = @avatar
				WHERE identifier = @userId
				";
			var sqlParams = new {Avatar = avatar, request.UserId};
			await _unitOfWork.Connection.ExecuteAsync(sql, sqlParams, _unitOfWork.Transaction);
		}
	}
}