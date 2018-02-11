using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Domain.UserSubscriptions;
using KodisoftApp.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace KodisoftApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class SubscriptionsController : Controller
    {
        private readonly IUserSubscriptionsRepository _userSettingsRepository;
        private readonly IUserIdProvider _userIdProvider;

        public SubscriptionsController(IUserSubscriptionsRepository userSettingsRepository, IUserIdProvider userIdProvider)
        {
            _userSettingsRepository = userSettingsRepository;
            _userIdProvider = userIdProvider;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> GetSubscriptions()
        {
            var id = _userIdProvider.GetId(User);
            return await _userSettingsRepository.GetSubscriptionsAsync(id).ToList();
        }

        [HttpPut]
        public async Task<IActionResult> Subscribe([FromQuery]QueryParams @params)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var id = _userIdProvider.GetId(User);
            await _userSettingsRepository.AddSubscriptionAsync(id, @params.SourceName);
            return Ok();

        }

        [HttpDelete]
        public async Task<IActionResult> Unsubscribe([FromQuery]QueryParams @params)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var id = _userIdProvider.GetId(User);
            await _userSettingsRepository.RemoveSubscriptionAsync(id, @params.SourceName);
            return Ok();
        }

        public class QueryParams
        {
            [BindRequired]
            public string SourceName { get; set; }
        }
     }
}