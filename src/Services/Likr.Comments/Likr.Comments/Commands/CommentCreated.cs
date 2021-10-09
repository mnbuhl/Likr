using System;

// ReSharper disable once CheckNamespace
namespace Likr.Commands
{
    public record CommentCreated(string Id, string Body, string UserId, Guid PostId);
}