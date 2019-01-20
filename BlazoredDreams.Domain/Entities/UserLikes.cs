namespace BlazoredDreams.Domain.Entities
{
    public class UserLikes
    {
        public int UserId { get; set; }
        public int PostId { get; set; }

        public virtual Post Post { get; set; }
        public virtual IdentityUser User { get; set; }
    }
}
