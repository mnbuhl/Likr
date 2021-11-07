namespace Likr.Client.Dtos;

public record CommentDto(
    string Id,
    DateTime CreatedAt,
    string Body,
    string UserId,
    Guid PostId,
    int LikesCount,
    int CommentsCount,
    ICollection<CommentDto> Comments);