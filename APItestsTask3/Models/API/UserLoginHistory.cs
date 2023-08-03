namespace APItestsTask3.Models.API
{
    public class UserLoginHistory
    {
        public string UserName { get; set; } = default!;

        public int LoginFailCount { get; set; }
    }
}
