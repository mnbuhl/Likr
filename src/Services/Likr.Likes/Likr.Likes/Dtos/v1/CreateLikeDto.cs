using System.ComponentModel.DataAnnotations;

namespace Likr.Likes.Dtos.v1
{
    public record CreateLikeDto([Required] string ObserverId, [Required] string TargetId);
}