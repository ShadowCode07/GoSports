using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GoSportsAPI.Extensions
{
    public static class ClaimsPrincipalExtensions
    {

        public static Guid? GetUserId(this ClaimsPrincipal user)
        {
            // Try usual places JWT/Identity store the user id
            var idValue =
                user.FindFirstValue(ClaimTypes.NameIdentifier) ??
                user.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (Guid.TryParse(idValue, out var guid))
                return guid;

            return null;
        }

        public static string? FindFirstValue(this ClaimsPrincipal user, string claimType)
        {
            return user.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;
        }
    }
}
