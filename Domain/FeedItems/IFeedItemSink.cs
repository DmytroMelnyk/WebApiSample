using System.Threading.Tasks;

namespace Domain.FeedItems
{
    public interface IFeedItemSink
    {
        Task OnNextFeedItemAsync(FeedItem item, string sourceName);
    }
}