using System;
using CloudinaryDotNet;

namespace BlazoredDreams.Application.Interfaces
{
	public interface ICloudinaryService
	{
		string Upload(FileDescription file);
	}
}