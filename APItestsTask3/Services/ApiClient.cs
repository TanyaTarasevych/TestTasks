using APItestsTask3.Services.Interfaces;
using RestSharp;

namespace APItestsTask3.Services
{
    public class ApiClient: IApiClient
    {
        private bool _disposedValue;

        public ApiClient(RestClient client)
        {
            Client = client;
        }

        ~ApiClient()
        {
            Dispose(false);
        }

        public RestClient Client { get; }

        public async Task<RestResponse<T>> GetAsync<T>(string uri, string token)
        {
            var request = new RestRequest(uri, Method.Get);

            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {token}");

            var response = await Client.ExecuteAsync(request);

            return Client.Deserialize<T>(response);
        }

        public async Task<RestResponse> PutAsync(string uri, string token, object? requestBody = null)
        {
            var request = new RestRequest(uri, Method.Put);

            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {token}");
            if (requestBody != null)
            {
                request.AddJsonBody(requestBody);
            }

            var response = await Client.ExecuteAsync(request);

            return response;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue)
            {
                return;
            }

            if (disposing)
            {
                Client.Dispose();
            }

            _disposedValue = true;
        }
    }
}
