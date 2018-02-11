using System.Security.Claims;

namespace KodisoftApp.Infrastructure
{
    internal class UserIdProvider : IUserIdProvider
    {
        public string GetId(ClaimsPrincipal user) => 
            user.FindFirst(x => x.Type == ClaimTypes.Email).Value;
    }
}