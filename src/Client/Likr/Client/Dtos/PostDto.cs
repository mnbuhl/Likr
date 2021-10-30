namespace Likr.Client.Dtos;

public record PostDto(string Id, string Body, string UserId, int LikesCount, int CommentsCount,
    ICollection<CommentDto> Comments);