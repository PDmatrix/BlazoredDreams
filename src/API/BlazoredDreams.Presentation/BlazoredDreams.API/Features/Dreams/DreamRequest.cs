using System;
using FluentValidation;

namespace BlazoredDreams.API.Features.Dreams
{
	public class DreamRequest
	{
		public string Content { get; set; }
		public DateTime Date { get; set; }
	}
	
	// ReSharper disable once UnusedMember.Global
	public class DreamRequestValidator : AbstractValidator<DreamRequest>
	{
		public DreamRequestValidator()
		{
			RuleFor(r => r.Content).NotEmpty();
			RuleFor(r => r.Date).NotEmpty();
		}
	}
}