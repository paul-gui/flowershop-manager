using System.Security.Claims;

namespace FlowerShopAPI.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static List<string> GetRoles(this ClaimsPrincipal user)
        {
            return user.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();
        }

        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
