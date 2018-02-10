﻿using System.Collections.Generic;
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
        public IEnumerable<string> Get()
        {
            return _feedItemSourceProvider.GetAll().Select(x => x.SourceName);
            //var mail = User.FindFirst(x => x.Type == ClaimTypes.Email).Value;
            //return User.Claims.Select(x => x.Value);
        }
    }
}
