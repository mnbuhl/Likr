using System;
using System.ComponentModel.DataAnnotations;

namespace Likr.Likes.Dtos.v1
{
    public record CreateLikeDto([Required] Guid ObserverId, [Required] Guid TargetId);
}