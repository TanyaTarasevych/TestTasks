using APItestsTask3.Models.DB;
using APItestsTask3.Seeders.Interfaces;

namespace APItestsTask3.Seeders
{
    //TODO
    public class UserLoginHistorySeeder: IUserLoginHistorySeeder
    {

        public Task<IEnumerable<UserLoginHistoryDao>> InsertAsync(IEnumerable<UserLoginHistoryDao> userLoginHistoryDaos)
        {
            throw new NotImplementedException();
        }

        public Task<UserLoginHistoryDao> GetAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync()
        {
            throw new NotImplementedException();
        }
    }
}
