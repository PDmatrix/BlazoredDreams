using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazoredDreams.Domain.Entities
{
    public class Tag : Entity<int>
    {
        public Tag()
        {
            PostTags = new HashSet<PostTags>();
        }

        public string Name { get; set; }

        public virtual ICollection<PostTags> PostTags { get; set; }
    }
}
