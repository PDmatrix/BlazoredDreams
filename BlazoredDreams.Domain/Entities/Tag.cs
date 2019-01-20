using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazoredDreams.Domain.Entities
{
    [Table("tag")]
    public class Tag
    {
        public Tag()
        {
            PostTags = new HashSet<PostTags>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<PostTags> PostTags { get; set; }
    }
}
