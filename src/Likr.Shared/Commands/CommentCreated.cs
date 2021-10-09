using System;

namespace Likr.Comments.Dtos.v1
{
    public record CommentCreated(string Id, string Body, string UserId, Guid PostId);
}