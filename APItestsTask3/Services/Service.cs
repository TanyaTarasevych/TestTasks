using APItestsTask3.Services.Interfaces;
using APItestsTask3.Configuration;
using APItestsTask3.Models.API;
using RestSharp;

namespace APItestsTask3.Services
{
    public class Service : IService
    {
        private static readonly string _apiVersion = AppSettings.Current.WebApi.ApiVersion!;
        private readonly IApiClient _client;

        public Service(IApiClient apiClient)
        {
            _client = apiClient;
        }

        public async Task<RestResponse<IEnumerable<UserLoginHistory>>> GetLoginFailTotal(string token, string? query)
        {
            var uri = $"{_apiVersion}/loginfailtotal{query}";

            return await _client.GetAsync<IEnumerable<UserLoginHistory>>(uri, token);
        }

        public async Task<RestResponse> PutResetLoginFailTotal(string token, string query)
        {
            var uri = $"{_apiVersion}/resetloginfailtotal{query}";

            return await _client.PutAsync(uri, token);
        }
    }
}
