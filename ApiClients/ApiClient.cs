using RestSharp;

namespace RestApiNUnitTests.ApiClients
{
    public class ApiClient
    {
        private readonly RestClient _client;

        public ApiClient(string baseUrl)
        {
            _client = new RestClient(baseUrl);
        }

        public async Task<RestResponse> SendRequestAsync<T>(string endpoint, Method method, T body) where T : class
        {
            var request = new RestRequest(endpoint, method);
            request.AddHeader("Content-Type", "application/json");

            if (body != null)
            {
                request.AddJsonBody(body);
            }

            return await _client.ExecuteAsync(request);
        }
    }
}
