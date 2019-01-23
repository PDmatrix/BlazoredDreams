using System;
using System.Collections.Generic;

namespace BlazoredDreams.Domain.Entities
{
    public class Dream : Entity<int>
    {
        public Dream()
        {
            Post = new HashSet<Post>();
        }

        public string Content { get; set; }
        public int UserId { get; set; }

        public virtual IdentityUser User { get; set; }
        public virtual ICollection<Post> Post { get; private set; }
    }
}
