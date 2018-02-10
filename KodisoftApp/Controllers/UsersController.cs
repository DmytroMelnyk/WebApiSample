using System.Security.Claims;
using System.Threading.Tasks;
using Domain;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KodisoftApp.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class UsersController : Controller
    {
        private readonly FeedItemSourceProvider _feedItemSourceProvider;
        private readonly IUserSettingsRepository _userSettingsRepository;

        public UsersController(FeedItemSourceProvider feedItemSourceProvider, IUserSettingsRepository userSettingsRepository)
        {
            _feedItemSourceProvider = feedItemSourceProvider;
            _userSettingsRepository = userSettingsRepository;
        }

        [HttpPut]
        public Task SubscribeAsync(string sourceName)
        {
            var mail = User.FindFirst(x => x.Type == ClaimTypes.Email).Value;
            return _userSettingsRepository.AddSubscriptionAsync(mail, sourceName);
        }
    }
}