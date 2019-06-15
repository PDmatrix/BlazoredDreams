using FluentValidation;

namespace BlazoredDreams.API.Features.Posts
{
	public class PostRequest
	{
		public string Title { get; set; }
		public string Excerpt { get; set; }
		public int DreamId { get; set; }
		public string Tags { get; set; }
	}

	// ReSharper disable once UnusedMember.Global
	public class PostRequestValidator : AbstractValidator<PostRequest>
	{
		public PostRequestValidator()
		{
			RuleFor(r => r.Title).NotEmpty();
			RuleFor(r => r.Excerpt).NotEmpty();
		}
	}
}