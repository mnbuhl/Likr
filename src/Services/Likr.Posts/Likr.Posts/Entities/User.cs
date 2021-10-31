using System.Collections.Generic;

namespace Likr.Posts.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string DisplayName { get; set; }
    }
}