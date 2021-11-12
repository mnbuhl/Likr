namespace Likr.Client.Dtos;

public class LikeDto
{
    public string? ObserverId { get; set; }
    public UserDto? Observer { get; set; }
    public string? TargetId { get; set; }
    public PostDto? Target { get; set; }
}