using System;
using System.Linq.Expressions;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using MongoDB.Driver;

namespace Infrastructure.Extensions
{
    public static class MongoCollectionExtensions
    {
        public static IObservable<T> FindAll<T>(this IMongoCollection<T> @this, Expression<Func<T, bool>> predicate, FindOptions<T> options = null) => Observable
            .FromAsync(ct => @this.FindAsync(predicate, options, ct))
            .SelectMany(cursor => Observable
                .FromAsync(cursor.MoveNextAsync, Scheduler.CurrentThread)
                .Repeat()
                .TakeWhile(x => x)
                .SelectMany(_ => cursor.Current));
    }
}