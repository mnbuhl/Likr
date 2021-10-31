using System;
using System.Collections.Generic;
using Likr.Comments.Entities;

namespace Likr.Comments.Dtos.v1
{
    public record CommentDto(
        string Id, 
        string Body, 
        User User, 
        Guid PostId, 
        int LikesCount,
        ICollection<CommentDto> Comments);
}