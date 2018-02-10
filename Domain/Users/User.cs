using System.Collections.Generic;

namespace Domain.Users
{
    public class User
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EMail { get; set; }

        public IEnumerable<string> Subscriptions { get; set; }
    }
}