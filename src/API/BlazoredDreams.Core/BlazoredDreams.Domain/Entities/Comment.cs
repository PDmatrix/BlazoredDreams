namespace BlazoredDreams.Domain.Entities
{
    public class Comment : Entity<int>
    {
        public string Content { get; set; }
        public int PostId { get; set; }
        public string UserId { get; set; }
    }
}
