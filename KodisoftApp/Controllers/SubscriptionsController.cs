using System.Collections.Generic;
using System.Reactive.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KodisoftApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class SubscriptionsController : Controller
    {
        private readonly IUserSubscriptionsRepository _userSettingsRepository;

        public SubscriptionsController(IUserSubscriptionsRepository userSettingsRepository)
        {
            _userSettingsRepository = userSettingsRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> GetSubscriptionsAsync()
        {
            var mail = User.FindFirst(x => x.Type == ClaimTypes.Email).Value;
            return await _userSettingsRepository.GetSubscriptionsAsync(mail).ToList();
        }

        [HttpPut("[action]")]
        public Task SubscribeAsync([FromQuery] string sourceName)
        {
            var mail = User.FindFirst(x => x.Type == ClaimTypes.Email).Value;
            return _userSettingsRepository.AddSubscriptionAsync(mail, sourceName);
        }
    }
}