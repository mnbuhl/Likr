using System.ComponentModel.DataAnnotations.Schema;

namespace Likr.Likes.Entities
{
    public class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
    }
}