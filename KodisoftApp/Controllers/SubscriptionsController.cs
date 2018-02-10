using System.Collections.Generic;
using System.Reactive.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Users;
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

        public SubscriptionsController(IUserSubscriptionsRepository userSettingsRepository)
        {
            _userSettingsRepository = userSettingsRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> GetSubscriptions()
        {
            var mail = User.FindFirst(x => x.Type == ClaimTypes.Email).Value;
            return await _userSettingsRepository.GetSubscriptionsAsync(mail).ToList();
        }

        [HttpPut]
        public async Task<IActionResult> Subscribe([FromQuery]QueryParams @params)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var mail = User.FindFirst(x => x.Type == ClaimTypes.Email).Value;
            await _userSettingsRepository.AddSubscriptionAsync(mail, @params.SourceName);
            return Ok();

        }

        [HttpDelete]
        public async Task<IActionResult> Unsubscribe([FromQuery]QueryParams @params)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var mail = User.FindFirst(x => x.Type == ClaimTypes.Email).Value;
            await _userSettingsRepository.RemoveSubscriptionAsync(mail, @params.SourceName);
            return Ok();
        }

        public class QueryParams
        {
            [BindRequired]
            public string SourceName { get; set; }
        }
     }
}