using System;

namespace Likr.Comments.Entities
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Body { get; set; }
        public string UserId { get; set; }
    }
}