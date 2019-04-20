namespace BlazoredDreams.Domain.Entities
{
    public class Comment : Entity<int>
    {
        public string Content { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
    }
}
