namespace Likr.Client.Dtos;

public class CommentDto
{
    public string? Id { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.Now;
    public string? Body { get; set; }
    public string? UserId { get; set; }
    public Guid? PostId { get; set; }
    public UserDto? User { get; set; }
    public int? LikesCount { get; set; }
    public ICollection<CommentDto>? Comments { get; set; } = new List<CommentDto>();
    public int? CommentsCount { get; set; }
}