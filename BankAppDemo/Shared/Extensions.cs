using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BankAppDemo.Shared 
{ 
    public static class Extensions
    {
        public static Guid GetIdentityId(this ClaimsPrincipal claimsPrincipal)
        {

            var iDIdentifier = claimsPrincipal.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
            return Guid.Parse(iDIdentifier.Value);
        }
    }
}
