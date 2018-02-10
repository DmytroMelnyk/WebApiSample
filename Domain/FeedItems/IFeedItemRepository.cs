using System;
using System.Threading.Tasks;

namespace Domain.FeedItems
{
    public interface IFeedItemRepository
    {
        Task<FeedItemHolder> GetAsync(string id);

        IObservable<FeedItemHolder> GetItems(string source);
    }
}
