using System;

namespace Likr.Commands
{
    public record CommentCreated(string Id, string Body, string UserId, string PostId);
}