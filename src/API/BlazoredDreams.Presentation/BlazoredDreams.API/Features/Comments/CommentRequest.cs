using FluentValidation;

namespace BlazoredDreams.API.Features.Comments
{
	public class CommentRequest
	{
		public string Content { get; set; }
	}
	
	// ReSharper disable once UnusedMember.Global
	public class CommentRequestValidator : AbstractValidator<CommentRequest>
	{
		public CommentRequestValidator()
		{
			RuleFor(r => r.Content).NotEmpty();
		}
	}
}