using APItestsTask3.Models.API;
using APItestsTask3.Models.DB;

namespace APItestsTask3.Utils.Mapping
{
    public static class UserLoginHistoryMapper
    {
        public static UserLoginHistory MapTo(UserLoginHistoryDao dao)
        {
            var userLoginHistory = new UserLoginHistory
            {
                UserName = dao.UserName,
                LoginFailCount = dao.LoginFailCount
            };

            return userLoginHistory;
        }
    }
}
