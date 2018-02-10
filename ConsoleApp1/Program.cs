using System;
using AutoMapper;
using Infrastructure;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace ConsoleApp1
{
    class Program
    {
        private static readonly ILoggerFactory LoggerFactory = new LoggerFactory().AddConsole(LogLevel.Trace);
        private static readonly IMongoDatabase MongoDatabase = new MongoClient("mongodb://localhost").GetDatabase("KodisoftApp");

        static Program()
        {
            Mapper.Initialize(x => x.AddProfile(new AutomapperProfile()));
        }

        static void Main(string[] args)
        {
            var source = new RssFeedSource(
                new FeedSettings
                {
                    SourceName = "BBC",
                    Url = "http://feeds.bbci.co.uk/news/world/rss.xml",
                    PollingIntervalMin = 1
                },
                Mapper.Instance,
                LoggerFactory.CreateLogger<RssFeedSource>());

            var repo = new FeedItemRepository(MongoDatabase);


            Console.ReadLine();
        }
    }
}
