using System.ComponentModel.DataAnnotations.Schema;

namespace Likr.Posts.Entities
{
    public abstract class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
    }
}