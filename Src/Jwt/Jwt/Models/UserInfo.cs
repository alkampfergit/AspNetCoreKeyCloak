using Microsoft.AspNetCore.Mvc;

namespace Jwt.Models
{
    public class UserInfo 
    {
        public static UserInfo Unauthenticated => new UserInfo();

        public Boolean IsAuthenticated { get; set; }
    }
}
