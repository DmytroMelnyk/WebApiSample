﻿using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Xml;
using AutoMapper;
using Domain.FeedItems;
using Infrastructure.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.SyndicationFeed.Rss;

namespace Infrastructure.FeedItems
{
    public class RssFeedSource : IFeedItemSource
    {
        private readonly ILogger<RssFeedSource> _logger;
        private readonly IObservable<FeedItem> _feedItemSourceImplementation;

        public RssFeedSource(FeedSettings settings, IMapper mapper, ILogger<RssFeedSource> logger)
        {
            var pollingInterval = TimeSpan.FromMinutes(settings.PollingIntervalMin);
            SourceName = settings.SourceName;
            _logger = logger;
            _feedItemSourceImplementation = Observable
                .Using(() => XmlReader.Create(settings.Url, new XmlReaderSettings { Async = true }), xmlReader =>
                    Observable.FromAsync(new RssFeedReader(xmlReader).ReadFeedItemAsync, Scheduler.CurrentThread)
                        .Repeat()
                        .TakeWhile(x => x != null))
                .Select(mapper.Map<FeedItem>)
                .DelaySubscription(pollingInterval)
                .Do(item => _logger.LogTrace($"Item with Id '{item.Id}' obtained from '{settings.SourceName}'."),
                    ex => _logger.LogError(ex, $"Error when reading rss feed with name '{settings.SourceName}' at address '{settings.Url}'."),
                    () => _logger.LogDebug($"Rss feed with name '{settings.SourceName}' at address '{settings.Url}' has just ended polling."))
                .Repeat()
                .Retry();
        }

        public string SourceName { get; }

        public IDisposable Subscribe(IFeedItemSink feedItemSink)
        {
            return _feedItemSourceImplementation.Subscribe(async item =>
            {
                try
                {
                    await feedItemSink.OnNextFeedItemAsync(item, SourceName);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, $"Feed item '{item.Id}' from source '{SourceName}' was not written to sink.");
                }
            });
        }
    }
}
