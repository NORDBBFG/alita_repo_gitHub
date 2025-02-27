using RestApiNUnitTests.Models;
using RestSharp;

namespace RestApiNUnitTests.ApiClients
{
    public class BookApiClient : ApiClient
    {
        private const string BaseUrl = "https://fakerestapi.azurewebsites.net/api/v1";

        public BookApiClient() : base(BaseUrl)
        { }

        public async Task<RestResponse> CreateBookAsync(Book book)
        {
            return await SendRequestAsync("/Books", Method.Post, book);
        }
    }
}
