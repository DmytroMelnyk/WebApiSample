using System.Collections.Generic;
using System.Reactive.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.FeedItems;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KodisoftApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class FeedItemsController : Controller
    {
        private readonly IFeedItemRepository _feedItemRepository;
        private readonly IUserSubscriptionsRepository _subscriptionsRepository;

        public FeedItemsController(IFeedItemRepository feedItemRepository, IUserSubscriptionsRepository subscriptionsRepository)
        {
            _feedItemRepository = feedItemRepository;
            _subscriptionsRepository = subscriptionsRepository;
        }

        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 3600)]
        public async Task<IEnumerable<FeedItem>> Get()
        {
            var mail = User.FindFirst(x => x.Type == ClaimTypes.Email).Value;
            return await _subscriptionsRepository
                .GetSubscriptionsAsync(mail)
                .SelectMany(_feedItemRepository.GetItems)
                .ToList();
        } 
    }
}