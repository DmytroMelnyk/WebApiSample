using System;
using System.Reactive.Linq;
using Infrastructure;
using Microsoft.SyndicationFeed;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomRssFeedReader
                .ReadRssItems("http://feeds.bbci.co.uk/news/world/rss.xml")
                .Subscribe(x => Process(x), () =>
            {

            });
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }

        static void Process(object item)
        {

        }

        static void Process(ISyndicationItem item)
        {

        }
    }
}
