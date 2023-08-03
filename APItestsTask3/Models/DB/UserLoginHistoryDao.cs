namespace APItestsTask3.Models.DB
{
    public class UserLoginHistoryDao
    {
        public string UserName { get; set; } = default!;

        public int LoginFailCount { get; set; }
    }
}
