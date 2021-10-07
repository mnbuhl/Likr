using System;

namespace Likr.Comments.Dtos.v1
{
    public record CreateCommentDto(string Body, string UserId, Guid PostId);
}