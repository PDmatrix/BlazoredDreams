using System;
using System.Collections.Generic;

namespace BlazoredDreams.Domain.Entities
{
    public class Post : Entity<int>
    {
        public Post()
        {
            Comment = new HashSet<Comment>();
            PostTags = new HashSet<PostTags>();
        }

        public string Title { get; set; }
        public int UserId { get; set; }
        public int DreamId { get; set; }

        public Dream Dream { get; set; }
        public IdentityUser User { get; set; }
        public UserLikes UserLikes { get; set; }
        public ICollection<Comment> Comment { get; private set; }
        public ICollection<PostTags> PostTags { get; private set; }
    }
}
