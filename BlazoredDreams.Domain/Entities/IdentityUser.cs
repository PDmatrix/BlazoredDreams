using System;
using System.Collections.Generic;

namespace BlazoredDreams.Domain.Entities
{
    public class IdentityUser
    {
        public IdentityUser()
        {
            Comment = new HashSet<Comment>();
            Dream = new HashSet<Dream>();
            Post = new HashSet<Post>();
        }

        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Identifier { get; set; }

        public virtual UserLikes UserLikes { get; set; }
        public virtual ICollection<Comment> Comment { get; private set; }
        public virtual ICollection<Dream> Dream { get; private set; }
        public virtual ICollection<Post> Post { get; private set; }
    }
}
