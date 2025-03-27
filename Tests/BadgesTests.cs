using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace RestApiNUnitTests
{
    [TestFixture]
    public class BadgesTests
    {
        private HttpClient _client;
        private const string BaseUrl = "https://api.example.com"; // Replace with your API base URL

        [SetUp]
        public void Setup()
        {
            _client = new HttpClient();
        }

        [Test]
        public async Task GetBadges_ShouldReturnAllBadges()
        {
            // Act
            var response = await _client.GetAsync($"{BaseUrl}/api/v1/badges");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Assert.IsNotEmpty(content);
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
        }
    }
}
