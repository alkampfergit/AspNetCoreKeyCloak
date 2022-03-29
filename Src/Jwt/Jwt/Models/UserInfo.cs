using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Jwt.Models
{
    public class UserInfo 
    {
        public static UserInfo Unauthenticated => new UserInfo();

        public Boolean IsAuthenticated { get; set; }

        public string Name { get; set; }

        public IReadOnlyCollection<ClaimInfo> Claims { get; set; }
    }

    public class ClaimInfo 
    {
        public ClaimInfo(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}
