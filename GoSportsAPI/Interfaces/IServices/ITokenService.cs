using GoSportsAPI.Models.Users;

namespace GoSportsAPI.Interfaces.IServices
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
