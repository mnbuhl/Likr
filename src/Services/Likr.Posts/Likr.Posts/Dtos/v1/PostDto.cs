using System.Collections.Generic;
using Likr.Posts.Entities;

namespace Likr.Posts.Dtos.v1
{
    public record PostDto(string Id, string Body, User User, int LikesCount, int CommentsCount,
        ICollection<Comment> Comments);
}