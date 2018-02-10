using System.Collections.Generic;
using Autofac;
using Domain.FeedItems;
using Infrastructure;

namespace KodisoftApp
{
    public class FeedsModule : Module
    {
        public IEnumerable<FeedSettings> Settings { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            foreach (var feedSettings in Settings)
            {
                builder.RegisterType<RssFeedSource>()
                    .As<IFeedItemSource>()
                    .WithParameter(new TypedParameter(typeof(FeedSettings), feedSettings))
                    .SingleInstance();
            }
        }
    }
}
