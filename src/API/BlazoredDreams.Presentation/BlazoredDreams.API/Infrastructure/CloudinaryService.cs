using System;
using BlazoredDreams.Application.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace BlazoredDreams.API.Infrastructure
{
	public class CloudinaryService : ICloudinaryService
	{
		private readonly Cloudinary _cloudinary;
		
		public CloudinaryService(string cloudinaryUrl)
		{
			_cloudinary = new Cloudinary(cloudinaryUrl);
		}

		public string Upload(FileDescription file)
		{
			var response = _cloudinary.Upload(new ImageUploadParams
			{
				File = file
			});
			return response.SecureUri.ToString();
		}
	}
}