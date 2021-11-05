using System.ComponentModel.DataAnnotations;

namespace Likr.Client.Dtos;

public class CreatePostDto
{
    [Required]
    public string? Body { get; set; }
    [Required]
    public Guid UserId { get; set; }
}