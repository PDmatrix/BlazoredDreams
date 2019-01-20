using System;
using System.Collections.Generic;

namespace BlazoredDreams.Domain.Entities
{
    public class Dream
    {
        public Dream()
        {
            Post = new HashSet<Post>();
        }

        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UserId { get; set; }

        public virtual IdentityUser User { get; set; }
        public virtual ICollection<Post> Post { get; private set; }
    }
}
