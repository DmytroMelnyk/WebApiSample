using System;
using System.Threading.Tasks;

namespace Domain.UserSubscriptions
{
    public interface IUserSubscriptionsRepository
    {
        Task AddSubscriptionAsync(string userId, string subscription);

        Task RemoveSubscriptionAsync(string userId, string subscription);

        IObservable<string> GetSubscriptionsAsync(string userId);
    }
}