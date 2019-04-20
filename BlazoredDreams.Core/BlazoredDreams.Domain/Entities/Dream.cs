namespace BlazoredDreams.Domain.Entities
{
    public class Dream : Entity<int>
    {
        public string Content { get; set; }
        public int UserId { get; set; }
    }
}
