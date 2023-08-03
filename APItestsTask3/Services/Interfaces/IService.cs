using APItestsTask3.Models.API;
using RestSharp;

namespace APItestsTask3.Services.Interfaces
{
    public interface IService
    {
        public Task<RestResponse<IEnumerable<UserLoginHistory>>> GetLoginFailTotal(string token, string? query = null);

        public Task<RestResponse> PutResetLoginFailTotal(string token, string query);
    }
}
