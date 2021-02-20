using System.Security.Claims;

namespace PathCase.MVC.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetName(this ClaimsPrincipal principal)
        {
            return principal.Identity?.Name;
        }
    }
}