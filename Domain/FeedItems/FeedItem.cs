using System;
using System.Collections.Generic;

namespace Domain.FeedItems
{
    public class FeedItem
    {
        public FeedItem(
            string id, 
            string title,
            string description,
            IEnumerable<string> categories,
            IEnumerable<FeedAuthor> contributors,
            IEnumerable<FeedLink> links,
            DateTimeOffset published)
        {
            Id = id;
            Title = title;
            Description = description;
            Categories = categories;
            Contributors = contributors;
            Links = links;
            Published = published;
        }

        public string Id { get; }
        public string Title { get; }
        public string Description { get; }
        public IEnumerable<string> Categories { get; }
        public IEnumerable<FeedAuthor> Contributors { get; }
        public IEnumerable<FeedLink> Links { get; }
        public DateTimeOffset Published { get; }
    }
}