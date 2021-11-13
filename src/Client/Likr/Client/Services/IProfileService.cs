using Likr.Client.Dtos;

namespace Likr.Client.Services;

public interface IProfileService
{
    public Task<UserDto?> GetProfileByUsername(string username);
}