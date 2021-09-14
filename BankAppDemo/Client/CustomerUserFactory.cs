using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

public class CustomUserFactory
    : AccountClaimsPrincipalFactory<RemoteUserAccount>
{
    public CustomUserFactory(IAccessTokenProviderAccessor accessor)
        : base(accessor)
    {
    }

    public async override ValueTask<ClaimsPrincipal> CreateUserAsync(
        RemoteUserAccount account,
        RemoteAuthenticationUserOptions options)
    {
        var user = await base.CreateUserAsync(account, options);

        if (user.Identity.IsAuthenticated)
        {
            var identity = (ClaimsIdentity)user.Identity;


            if (identity.Claims.Where(x => x.Type == ClaimTypes.Role && x.Value == "admin").Any())
            {
                var adminClaim = new Claim(ClaimTypes.Role, "admin");
                var claimsIdentity = new ClaimsIdentity();
                claimsIdentity.AddClaim(adminClaim);
                user.AddIdentity(claimsIdentity);
                Console.WriteLine("Användaren" + user.Identity.Name + " är admin ");
            }

            if (identity.Claims.Where(x => x.Type == ClaimTypes.Role && x.Value == "customer").Any())
            {
                var adminClaim = new Claim(ClaimTypes.Role, "customer");
                var claimsIdentity = new ClaimsIdentity();
                claimsIdentity.AddClaim(adminClaim);
                user.AddIdentity(claimsIdentity);
                Console.WriteLine("Användaren" + user.Identity.Name + " är customer ");
            }

            if (identity.Claims.Where(x => x.Type == ClaimTypes.Role && x.Value == "pending").Any())
            {
                var adminClaim = new Claim(ClaimTypes.Role, "pending");
                var claimsIdentity = new ClaimsIdentity();
                claimsIdentity.AddClaim(adminClaim);
                user.AddIdentity(claimsIdentity);
                Console.WriteLine("Användaren" + user.Identity.Name + " pending ");
            }
        }

        return user;
    }
}
