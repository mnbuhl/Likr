using System.ComponentModel.DataAnnotations.Schema;
using Likr.Likes.Interfaces;

namespace Likr.Likes.Entities
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Biography { get; set; }
    }
}