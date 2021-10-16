using System;
using System.ComponentModel.DataAnnotations;

namespace Likr.Comments.Dtos.v1
{
    public record CreateCommentDto(
        [Required, MaxLength(280)] string Body, 
        [Required, MaxLength(36), MinLength(36)] string UserId, 
        Guid PostId
        );
}