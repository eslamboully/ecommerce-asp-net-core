using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using ecommerceAspCore.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Linq;
using Microsoft.AspNetCore.Authentication;

namespace ecommerceAspCore.Security
{
    public class SecurityManager
    {
        public async void SignIn(HttpContext httpContext, Account account)
        {
            var getAllUserClaims = getUserClaims(account);
            var cookieAdminIdentity = new ClaimsIdentity(getAllUserClaims,"cookieAdmin");
            var claimsPrincipal = new ClaimsPrincipal(cookieAdminIdentity);

            httpContext.SignInAsync(claimsPrincipal);
        }

        public async void SignOut(HttpContext httpContext)
        {
            await httpContext.SignOutAsync();
        }

        private IEnumerable<Claim> getUserClaims(Account account)
        {
            var cookieAdminClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,account.Username),
                new Claim(ClaimTypes.Name,account.Email),
            };
            account.RoleAccounts.ToList().ForEach(RoleAccount => 
            {
                cookieAdminClaims.Add(new Claim(ClaimTypes.Role, RoleAccount.Role.Name));
            });

            return cookieAdminClaims;
        }
    }
}