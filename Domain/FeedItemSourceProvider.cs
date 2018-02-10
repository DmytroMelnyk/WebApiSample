using System.Collections.Generic;
using System.Linq;
using Domain.FeedItems;

namespace Domain
{
    public class FeedItemSourceProvider
    {
        private readonly IReadOnlyDictionary<string, IFeedItemSource> _feedSources;

        public FeedItemSourceProvider(IEnumerable<IFeedItemSource> feedSources)
        {
            _feedSources = feedSources.ToDictionary(x => x.SourceName);
        }

        public IFeedItemSource this[string sourceName] => _feedSources[sourceName];

        public IEnumerable<IFeedItemSource> GetAll() => _feedSources.Values;
    }
}