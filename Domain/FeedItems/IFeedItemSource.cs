using System;

namespace Domain.FeedItems
{
    public interface IFeedItemSource
    {
        string SourceName { get; }

        IDisposable Subscribe(IFeedItemSink feedItemSink);
    }
}