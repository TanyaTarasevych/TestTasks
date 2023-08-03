using APItestsTask3.Configuration;
using APItestsTask3.Seeders;
using APItestsTask3.Seeders.Interfaces;
using APItestsTask3.Services;
using APItestsTask3.Services.Interfaces;
using NUnit.Framework;
using RestSharp;

namespace APItestsTask3.Tests
{
    public class BaseTest
    {
        protected readonly IUserLoginHistorySeeder _userLoginHistorySeeder;
        protected readonly IApiClient _apiClient;
        protected readonly IService _service;
        protected readonly ITokenGenerationService _tokenGenerationService;
        protected readonly TestDataStorage.TestDataStorage _testDataStorage;

        public BaseTest()
        {
            _userLoginHistorySeeder = new UserLoginHistorySeeder();
            _apiClient = new ApiClient(new RestClient(AppSettings.Current.WebApi.BaseAddress));
            _service = new Service(_apiClient);
            _tokenGenerationService = new TokenGenerationService();
            _testDataStorage = new TestDataStorage.TestDataStorage();
        }

        [SetUp, Order(1)]
        public async Task ClearDbTable()
        {
            await _userLoginHistorySeeder.DeleteAsync();
        }

        //TODO: update to support multiple users, refresh, etc
        [OneTimeSetUp]
        public void GetBearerToken()
        {
            _testDataStorage.Token = _tokenGenerationService.GetAccessToken();
        }
    }
}
