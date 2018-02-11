using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.FeedItems;
using KodisoftApp.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KodisoftApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class FeedItemsController : Controller
    {
        private readonly FeedItemsProvider _feedItemsProvider;
        private readonly IUserIdProvider _userIdProvider;

        public FeedItemsController(FeedItemsProvider feedItemsProvider, IUserIdProvider userIdProvider)
        {
            _feedItemsProvider = feedItemsProvider;
            _userIdProvider = userIdProvider;
        }

        [HttpGet]
        public async Task<IEnumerable<FeedItem>> Get()
        {
            var id = _userIdProvider.GetId(User);
            return await _feedItemsProvider.GetFeedItems(id).ToList();
        } 
    }
}