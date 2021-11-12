using System.ComponentModel.DataAnnotations;

namespace Likr.Client.Dtos;

public class CreateCommentDto
{
    [Required, MinLength(1), MaxLength(280)] 
    public string? Body { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid PostId { get; set; }
}