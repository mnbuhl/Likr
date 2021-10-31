namespace Likr.Client.Dtos;

public record CommentDto(
    string Id,
    string Body,
    string UserId,
    Guid PostId,
    int LikesCount,
    ICollection<CommentDto> Comments);