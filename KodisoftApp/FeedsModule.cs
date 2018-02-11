using System.Collections.Generic;
using Autofac;
using Domain;
using Domain.FeedItems;
using Infrastructure.FeedItems;
using KodisoftApp.Infrastructure;

namespace KodisoftApp
{
    public class FeedsModule : Module
    {
        public IEnumerable<FeedSettings> Settings { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserIdProvider>()
                .As<IUserIdProvider>()
                .InstancePerLifetimeScope();

            builder.RegisterType<FeedItemSourceProvider>()
                .AsSelf()
                .SingleInstance();

            builder.RegisterType<FeedItemSourceSubscriptionManager>()
                .AsSelf()
                .SingleInstance()
                .AutoActivate();

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
