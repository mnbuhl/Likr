using System;
using System.Collections.Generic;

namespace Likr.Comments.Dtos.v1
{
    public record CommentDto(
        string Id, 
        string Body, 
        string UserId, 
        Guid PostId, 
        int LikesCount,
        ICollection<CommentDto> Comments);
}