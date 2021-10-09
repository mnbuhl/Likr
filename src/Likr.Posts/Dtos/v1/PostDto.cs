using System;
using System.Collections.Generic;
using Likr.Posts.Entities;

namespace Likr.Posts.Dtos.v1
{
    public record PostDto(Guid Id, string Body, string UserId, int LikesCount, ICollection<Comment> Comments);
}