using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Domain.FeedItems;
using Microsoft.SyndicationFeed;

namespace Infrastructure
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<ISyndicationItem, FeedItem>().ConstructUsing((item, ctx) => new FeedItem(
                item.Id,
                item.Title,
                item.Description,
                item.Categories.Select(x => x.Name).ToList(),
                ctx.Mapper.Map<IEnumerable<FeedAuthor>>(item.Contributors),
                ctx.Mapper.Map<IEnumerable<FeedLink>>(item.Links),
                item.Published));

            CreateMap<ISyndicationPerson, FeedAuthor>()
                .ConstructUsing(x => new FeedAuthor(x.Email, x.Name));

            CreateMap<ISyndicationLink, FeedLink>()
                .ConstructUsing(x => new FeedLink(x.Uri, x.Title, x.LastUpdated));
        }
    }
}
