namespace Likr.Client.Dtos;

public record CommentDto(
    string Id,
    DateTime CreatedAt,
    string Body,
    string UserId,
    Guid PostId,
    UserDto User,
    int LikesCount,
    int CommentsCount,
    ICollection<CommentDto> Comments);