namespace Likr.Client.Dtos;

public class PostDto
{
    public string Id { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public UserDto? User { get; set; }
    public int LikesCount { get; set; }
    public int CommentsCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<CommentDto> Comments { get; set; } = new List<CommentDto>();
}