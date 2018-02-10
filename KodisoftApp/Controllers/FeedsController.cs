using System.Collections.Generic;
using System.Linq;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KodisoftApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class FeedsController : Controller
    {
        private readonly FeedItemSourceProvider _feedItemSourceProvider;

        public FeedsController(FeedItemSourceProvider feedItemSourceProvider)
        {
            _feedItemSourceProvider = feedItemSourceProvider;
        }

        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 3600)]
        public IEnumerable<string> Get() => _feedItemSourceProvider
            .GetAll()
            .Select(x => x.SourceName);
    }
}
