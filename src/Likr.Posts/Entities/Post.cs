using System.Collections.Generic;

namespace Likr.Posts.Entities
{
    public class Post
    {
        public string Id { get; set; }
        public string Body { get; set; }
        public string UserId { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}