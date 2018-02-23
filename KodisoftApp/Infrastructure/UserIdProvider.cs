using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace KodisoftApp.Infrastructure
{
    internal class UserIdProvider : IUserIdProvider
    {
        public UserIdProvider(IHttpContextAccessor contextAccessor)
        {
            User = contextAccessor.HttpContext.User.FindFirst(x => x.Type == ClaimTypes.Email).Value;
        }

        public string User { get; }
    }
}