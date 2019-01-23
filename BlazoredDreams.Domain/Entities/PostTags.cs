﻿using System.ComponentModel.DataAnnotations.Schema;

namespace BlazoredDreams.Domain.Entities
{
    public class PostTags
    {
        public int PostId { get; set; }
        public int TagId { get; set; }

        public virtual Post Post { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
