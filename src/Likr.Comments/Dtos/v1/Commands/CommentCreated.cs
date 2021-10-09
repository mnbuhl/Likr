using System;

namespace Likr.Comments.Dtos.v1.Commands
{
    public record CommentCreated(string Id, string Body, string UserId, Guid PostId);
}