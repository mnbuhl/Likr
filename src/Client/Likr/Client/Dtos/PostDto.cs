namespace Likr.Client.Dtos;

public class PostDto
{
    public string? Id { get; set; }
    public string? Body { get; set; }
    public string? UserId { get; set; }
    public UserDto? User { get; set; }
    public int LikesCount { get; set; }
    public int CommentsCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<CommentDto>? Comments { get; set; }
}