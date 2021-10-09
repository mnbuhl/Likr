using System;

namespace Likr.Comments.Commands
{
    public record CommentCreated(string Id, string Body, string UserId, Guid PostId);
}