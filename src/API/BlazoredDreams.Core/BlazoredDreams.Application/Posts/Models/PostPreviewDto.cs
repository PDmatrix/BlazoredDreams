namespace BlazoredDreams.Application.Posts.Models
{
	public class PostPreviewDto
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string Title { get; set; }
		public int Comments { get; set; }
		public string Excerpt { get; set; }
		public string Date { get; set; }
		public string Tag { get; set; }
	}
}