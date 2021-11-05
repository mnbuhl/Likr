namespace Likr.Client.Dtos;

public record PostDto(string Id, string Body, string UserId, UserDto User, int LikesCount, int CommentsCount, DateTime CreatedAt,
    ICollection<CommentDto> Comments);