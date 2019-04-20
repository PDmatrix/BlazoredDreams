namespace BlazoredDreams.Domain.Entities
{
    public class Post : Entity<int>
    {
        public string Title { get; set; }
        public int UserId { get; set; }
        public int DreamId { get; set; }
        public string Excerpt { get; set; }
    }
}
