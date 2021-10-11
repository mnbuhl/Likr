using Likr.Likes.Entities;

namespace Likr.Likes.Dtos.v1
{
    public record LikeDto(string Id, string ObserverId, User Observer, string TargetId, Post Target);
}