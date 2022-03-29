using Jwt.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Jwt.Controllers
{
    [ApiController]
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
            if (ClaimsPrincipal.Current == null)
            {
                return UserInfo.Unauthenticated;
            }

            return new UserInfo()
            {
                IsAuthenticated = ClaimsPrincipal.Current.Identity.IsAuthenticated
            };
        }
    }
}
