using System;
using System.Collections;
using System.Collections.Generic;
using Likr.Comments.Entities;

namespace Likr.Comments.Dtos.v1
{
    public record CommentDto(string Id, string Body, string UserId, Guid PostId, ICollection<CommentDto> Comments);
}