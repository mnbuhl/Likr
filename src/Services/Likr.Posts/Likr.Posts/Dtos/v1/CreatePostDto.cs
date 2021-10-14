using System.ComponentModel.DataAnnotations;

namespace Likr.Posts.Dtos.v1
{
    public record CreatePostDto(
        [Required, MaxLength(280)] string Body, 
        [Required, MaxLength(36), MinLength(36)] string UserId
        );
}