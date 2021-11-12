using Likr.Client.Dtos;

namespace Likr.Client.Services;

public interface ILikeService
{
    Task<IList<LikeDto>?> GetLikesByPostId(string postId);
    Task<IList<LikeDto>?> GetLikesByUserId(string userId);
    Task<bool> Like(CreateLikeDto likeDto);
    Task<bool> Unlike(string postId);
}