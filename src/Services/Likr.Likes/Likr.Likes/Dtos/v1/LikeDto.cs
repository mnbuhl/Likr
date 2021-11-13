using System.Text.Json.Serialization;
using Likr.Likes.Entities;

namespace Likr.Likes.Dtos.v1
{
    public class LikeDto
    {
        public string ObserverId { get; init; }
        [JsonIgnore]
        public User Observer { get; init; }
        public string TargetId { get; init; }
        [JsonIgnore]
        public Post Target { get; init; }
    }
}