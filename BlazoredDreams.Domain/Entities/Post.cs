using System;
using System.Collections.Generic;

namespace BlazoredDreams.Domain.Entities
{
    public class Post
    {
        public Post()
        {
            Comment = new HashSet<Comment>();
            PostTags = new HashSet<PostTags>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int DreamId { get; set; }

        public Dream Dream { get; set; }
        public IdentityUser User { get; set; }
        public UserLikes UserLikes { get; set; }
        public ICollection<Comment> Comment { get; private set; }
        public ICollection<PostTags> PostTags { get; private set; }
    }
}
