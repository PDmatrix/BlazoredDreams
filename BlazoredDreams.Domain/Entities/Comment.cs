using System;

namespace BlazoredDreams.Domain.Entities
{
    public class Comment : Entity<int>
    {
        public string Content { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }

        public virtual Post Post { get; set; }
        public virtual IdentityUser User { get; set; }
    }
}
