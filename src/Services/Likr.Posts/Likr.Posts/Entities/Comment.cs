using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Likr.Posts.Entities
{
    public class Comment : BaseEntity
    {
        public string Body { get; set; }
        public string UserId { get; set; }
        public int LikesCount { get; set; }
        public Guid PostId { get; set; }
    }
}