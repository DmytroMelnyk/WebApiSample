using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure;
using Microsoft.SyndicationFeed;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Mapper.Initialize(x => x.AddProfile(new AutomapperProfile()));
            var source = new RssFeedSource(TimeSpan.FromSeconds(5), "http://feeds.bbci.co.uk/news/world/rss.xml", Mapper.Instance);

            source
                .Select(x => Observable.FromAsync(async () =>
            {
                    await Task.Delay(100);
                    Console.WriteLine(x.Id);
                
            }))
            .Concat()
            .Subscribe();

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
