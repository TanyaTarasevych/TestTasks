using RestSharp;

namespace APItestsTask3.Services.Interfaces
{
    public interface IApiClient: IDisposable
    {
        RestClient Client { get; }

        public Task<RestResponse<T>> GetAsync<T>(string uri, string token);

        public Task<RestResponse> PutAsync(string uri, string token, object? requestBody = null);
    }
}
