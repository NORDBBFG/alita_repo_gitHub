using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RestApiNUnitTests
{
    [TestFixture]
    public class BadgeTests
    {
        private HttpClient _client;
        private const string BaseUrl = "https://api.example.com"; // Replace with your API base URL

        [SetUp]
        public void Setup()
        {
            _client = new HttpClient();
        }

        [Test]
        public async Task PostBadge_ValidData_ReturnsOk()
        {
            // Arrange
            var badge = new { Name = "Test Badge", Description = "Test Description", Picture = "base64encodedstring" };
            var content = new StringContent(JsonConvert.SerializeObject(badge), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync($"{BaseUrl}/api/v1/badge", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task PutBadge_ValidData_ReturnsOk()
        {
            // Arrange
            var badge = new { Id = 1, Name = "Updated Badge", Description = "Updated Description", Picture = "base64encodedstring" };
            var content = new StringContent(JsonConvert.SerializeObject(badge), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync($"{BaseUrl}/api/v1/badge", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task GetBadges_ReturnsOk()
        {
            // Act
            var response = await _client.GetAsync($"{BaseUrl}/api/v1/badges");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task DeleteBadge_ValidId_ReturnsOk()
        {
            // Arrange
            int badgeId = 1; // Replace with a valid badge ID

            // Act
            var response = await _client.DeleteAsync($"{BaseUrl}/api/v1/badge/{badgeId}");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
        }
    }
}
