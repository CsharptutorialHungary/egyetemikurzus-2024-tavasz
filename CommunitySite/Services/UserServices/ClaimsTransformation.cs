using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace CommunitySite.Services.UserServices
{
    public class ClaimsTransformation : IClaimsTransformation
    {
        private readonly IUserService _userService;

        public ClaimsTransformation(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (principal is null || principal.Identity is null || !principal.Identity.IsAuthenticated)
            {
                return principal;
            }

            string username = principal.Identity.Name ?? "";
            if (!string.IsNullOrEmpty(username))
            {
                await _userService.EnsureUserExist(username);
            }

            return principal;
        }
    }
}
