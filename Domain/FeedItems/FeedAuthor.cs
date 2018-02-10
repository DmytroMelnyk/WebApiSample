namespace Domain.FeedItems
{
    public class FeedAuthor
    {
        public FeedAuthor(string email, string name)
        {
            Email = email;
            Name = name;
        }

        public string Email { get; }
        public string Name { get; }
    }
}
