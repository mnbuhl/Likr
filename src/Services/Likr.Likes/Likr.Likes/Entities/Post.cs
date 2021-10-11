using System.Collections.Generic;

namespace Likr.Likes.Entities
{
    public class Post : BaseEntity
    {
        public string Body { get; set; }
        public string UserId { get; set; }
        public ICollection<Like> Likes { get; set; }
    }
}