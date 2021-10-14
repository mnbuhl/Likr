using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Likr.Likes.Interfaces;

namespace Likr.Likes.Entities
{
    public class Post
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Body { get; set; }
        public string UserId { get; set; }
        public ICollection<Like> Likes { get; set; }
    }
}