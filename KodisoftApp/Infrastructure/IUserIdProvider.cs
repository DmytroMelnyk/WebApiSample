using System.Security.Claims;

namespace KodisoftApp.Infrastructure
{
    public interface IUserIdProvider
    {
        string GetId(ClaimsPrincipal user);
    }
}