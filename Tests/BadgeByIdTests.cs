using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace RestApiNUnitTests
{
    [TestFixture]
    public class BadgeByIdTests
    {
        private HttpClient _client;
        private const string BaseUrl = "https://api.example.com"; // Replace with your API base URL

        [SetUp]
        public void Setup()
        {
            _client = new HttpClient();
        }

        [Test]
        public async Task DeleteBadge_ShouldDeleteExistingBadge()
        {
            // Arrange
            int badgeId = 1; // Assume this badge exists

            // Act
            var response = await _client.DeleteAsync($"{BaseUrl}/api/v1/badge/{badgeId}");

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
