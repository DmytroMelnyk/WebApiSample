using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Users
{
    public interface IUserSettingsRepository
    {
        Task AddUserAsync(User user);

        Task AddSubscriptionAsync(string id, string subscription);

        Task<User> GetUserAsync(string id);
    }
}