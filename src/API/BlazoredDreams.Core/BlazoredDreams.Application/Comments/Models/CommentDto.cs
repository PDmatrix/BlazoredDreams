namespace BlazoredDreams.Application.Comments.Models
{
	public class CommentDto
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string Content { get; set; }
		public string Date { get; set; }
		public string UserId { get; set; }
		public string Avatar { get; set; }
	}
}