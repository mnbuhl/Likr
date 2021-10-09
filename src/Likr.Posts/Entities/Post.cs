using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Likr.Posts.Entities
{
    public class Post
    {
        [Key]
        public Guid Id { get; set; }
        public string Body { get; set; }
        public string UserId { get; set; }
        public int LikesCount { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}