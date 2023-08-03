using APItestsTask3.Models.DB;

namespace APItestsTask3.Seeders.Interfaces
{
    public interface IUserLoginHistorySeeder
    {
        Task<IEnumerable<UserLoginHistoryDao>> InsertAsync(IEnumerable<UserLoginHistoryDao> userLoginHistoryDaos);

        Task<UserLoginHistoryDao> GetAsync(string userName);

        Task DeleteAsync();
    }
}
