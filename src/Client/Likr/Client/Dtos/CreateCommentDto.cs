namespace Likr.Client.Dtos;

public record CreateCommentDto(string Body, Guid UserId, Guid PostId);