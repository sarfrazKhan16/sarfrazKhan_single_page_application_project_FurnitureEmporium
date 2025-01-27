using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

using System.Threading.Tasks;

namespace FurnitureEmporium.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private ClaimsPrincipal _authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity());

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return Task.FromResult(new AuthenticationState(_authenticatedUser));
        }

        public void MarkUserAsAuthenticated(string userName, string email, int userId)
        {
            var claims = new[]
            {
        new Claim(ClaimTypes.Name, userName),
        new Claim(ClaimTypes.Email, email),
        new Claim("UserId", userId.ToString()), // Add UserId claim
        new Claim(ClaimTypes.Role, userName == "Admin" ? "Admin" : "User") // Assign role
    };
            _authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(claims, "custom"));
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }



        public void MarkUserAsLoggedOut()
        {
            _authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
