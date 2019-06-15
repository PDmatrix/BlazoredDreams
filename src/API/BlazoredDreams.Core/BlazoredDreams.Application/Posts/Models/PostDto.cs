namespace BlazoredDreams.Application.Posts.Models
{
	public class PostDto
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string Title { get; set; }
		public string Date { get; set; }
		public string Tag { get; set; }
		public string Content { get; set; }
		public string Cover { get; set; }
		
		public int Comments { get; set; }
	}
}