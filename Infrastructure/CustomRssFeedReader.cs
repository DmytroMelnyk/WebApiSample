using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Xml;
using AutoMapper;
using Domain;
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Rss;

namespace Infrastructure
{
    public class RssFeedSource : IFeedItemSource
    {
        private readonly IObservable<FeedItem> _feedItemSourceImplementation;

        private static async Task<KeyValuePair<ISyndicationItem, bool>> ReadFeedAsync(ISyndicationFeedReader feedReader)
        {
            if (!await feedReader.Read())
            {
                return new KeyValuePair<ISyndicationItem, bool>(null, false);
            }

            return feedReader.ElementType == SyndicationElementType.Item ?
                new KeyValuePair<ISyndicationItem, bool>(await feedReader.ReadItem(), true) :
                new KeyValuePair<ISyndicationItem, bool>(null, true);
        }

        public RssFeedSource(TimeSpan pollingInterval, string uri, IMapper mapper)
        {
            _feedItemSourceImplementation = Observable
                .Using(() => XmlReader.Create(uri, new XmlReaderSettings { Async = true }), xmlReader =>
                    {
                        var feedReader = new RssFeedReader(xmlReader);
                        return Observable.FromAsync(() => ReadFeedAsync(feedReader), Scheduler.CurrentThread)
                            .Repeat()
                            .TakeWhile(x => x.Value)
                            .Where(x => x.Key != null)
                            .Select(x => x.Key);
                    })
                .Select(mapper.Map<FeedItem>)
                .Do(
                    x => Console.WriteLine(x.Title),
                    ex => Console.WriteLine(ex.Message),
                    () => Console.WriteLine("Completed"))
                .DelaySubscription(pollingInterval)
                .Repeat()
                .Retry()
                ;
        }

        public IDisposable Subscribe(IObserver<FeedItem> observer)
        {
            return _feedItemSourceImplementation.Subscribe(observer);
        }
    }
}
