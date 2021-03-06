﻿using System.Collections.Generic;
using Autofac;
using AutoMapper;
using Domain.FeedItems;
using Domain.UserSubscriptions;
using Infrastructure.FeedItems;
using Infrastructure.UserSubscriptions;
using MongoDB.Driver;

namespace Infrastructure
{
    public class InfrastructureModule : Module
    {
        public string ConnectionString { get; set; } = "mongodb://localhost";

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(_ => new MongoClient(ConnectionString))
                .As<IMongoClient>()
                .SingleInstance();

            builder.Register(ctx => ctx.Resolve<IMongoClient>().GetDatabase("Kodisoft"))
                .As<IMongoDatabase>()
                .SingleInstance();

            builder.RegisterType<FeedItemRepository>()
                .As<IFeedItemRepository>()
                .As<IFeedItemSink>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UserSubscriptionsRepository>()
                .As<IUserSubscriptionsRepository>()
                .As<IConfigurableRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterBuildCallback(x =>
            {
                foreach (var configurableRepository in x.Resolve<IEnumerable<IConfigurableRepository>>())
                {
                    configurableRepository.Configure();
                }
            });

            builder.RegisterType<AutomapperProfile>()
                .As<Profile>()
                .SingleInstance();

            builder.Register(c => new MapperConfiguration(cfg =>
                {
                    foreach (var profile in c.Resolve<IEnumerable<Profile>>())
                    {
                        cfg.AddProfile(profile);
                    }
                }))
                .AsSelf()
                .SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve<IComponentContext>().Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope();
        }
    }
}
