using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Movies.API.Services
{
    public class LoggedInUserService : ILoggedInUserService
    {
        private readonly ClaimsPrincipal _user;

        public LoggedInUserService(IHttpContextAccessor httpContextAccessor)
        {
            _user = httpContextAccessor.HttpContext?.User;
        }

        public string getName()
        {
            return _user?.Identity?.Name;
        }

        public string getUserId()
        {
            return _user?
                .Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?
                .Value;
        }
    }
}
