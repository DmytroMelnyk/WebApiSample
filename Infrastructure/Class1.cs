using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Xml;
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Rss;

namespace Infrastructure
{
    public class CustomRssFeedReader
    {
        public static IObservable<ISyndicationItem> ReadRssItems(string uri)
        {
            return Observable.Using(() => XmlReader.Create(uri, new XmlReaderSettings { Async = true }), xmlReader =>
            {
                var feedReader = new RssFeedReader(xmlReader);
                return Observable.FromAsync(feedReader.Read, Scheduler.CurrentThread)
                    .Repeat()
                    .TakeWhile(x => x)
                    .Where(_ => feedReader.ElementType == SyndicationElementType.Item)
                    .SelectMany(x => feedReader.ReadItem());
            });
        }
    }
}
