using BankAppDemo.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BankAppDemo.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet("getcurrentuser")]
        public async Task<ActionResult<ApplicationUser>> GetCurrentUser()
        {
            ApplicationUser currentUser = new ApplicationUser();


            if (User.Identity.IsAuthenticated)
            {
                Console.WriteLine("Är auth");
                currentUser.UserName = User.FindFirstValue(ClaimTypes.Email);
            }

            return await Task.FromResult(currentUser);
        }
    }
}
