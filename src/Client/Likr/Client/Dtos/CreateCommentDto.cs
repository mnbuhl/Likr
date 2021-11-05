using System.ComponentModel.DataAnnotations;

namespace Likr.Client.Dtos;

public record CreateCommentDto([Required] string Body, [Required] Guid UserId, [Required] Guid PostId);