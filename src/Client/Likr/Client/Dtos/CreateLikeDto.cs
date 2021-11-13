using System.ComponentModel.DataAnnotations;

namespace Likr.Client.Dtos;

public class CreateLikeDto
{
    [Required]
    public string? ObserverId { get; set; }
    [Required]
    public string? TargetId { get; set; }
}