using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Likr.Posts.Entities
{
    public class Post : BaseEntity
    {
        public string Body { get; set; }
        public string UserId { get; set; }
        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }
        
        [NotMapped]
        public ICollection<Comment> Comments { get; set; }
    }
}