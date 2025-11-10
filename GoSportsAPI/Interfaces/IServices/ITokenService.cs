using GoSportsAPI.Models.Users;

namespace GoSportsAPI.Interfaces.IServices
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}
