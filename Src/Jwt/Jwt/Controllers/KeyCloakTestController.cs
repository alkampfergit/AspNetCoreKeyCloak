using Jwt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Jwt.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class KeyCloakTestController : ControllerBase
    {
        [HttpGet(Name = "Peek")]
        [Route("Peek")]
        public Object Peek()
        {
            return new
            {
                CurrentUser = GetCurrentUserInfo()
            };
        }

        private object GetCurrentUserInfo()
        {
            if (HttpContext.User?.Identity == null)
            {
                return UserInfo.Unauthenticated;
            }

            var claimsPrincipal = HttpContext.User;
            return new UserInfo()
            {
                IsAuthenticated = claimsPrincipal.Identity.IsAuthenticated,
                Name = claimsPrincipal.Identity.Name!,
                Claims = claimsPrincipal.Claims
                    .Select(c => new ClaimInfo(c.Type, c.Value))
                    .ToArray()
            };
        }
    }
}
