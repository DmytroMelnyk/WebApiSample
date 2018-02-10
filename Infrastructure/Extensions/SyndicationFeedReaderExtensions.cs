using System.Threading.Tasks;
using Microsoft.SyndicationFeed;

namespace Infrastructure.Extensions
{
    public static class SyndicationFeedReaderExtensions
    {
        public static async Task<ISyndicationItem> ReadFeedItemAsync(this ISyndicationFeedReader @this)
        {
            while (await @this.Read())
            {
                if (@this.ElementType == SyndicationElementType.Item)
                {
                    var feedItem = await @this.ReadItem();
                    if (feedItem != null)
                    {
                        return feedItem;
                    }
                }
                else
                {
                    await @this.Skip();
                }
            }

            return null;
        }
    }
}