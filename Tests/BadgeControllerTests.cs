using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DonorApp.Services.DTO;

namespace DonorApp.Tests
{
    [TestFixture]
    public class BadgeControllerTests
    {
        private HttpClient _client;

        [SetUp]
        public void Setup()
        {
            // Initialize the HttpClient with the appropriate base address
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://your-api-base-url/");
        }

        [Test]
        public async Task PostBadge_ValidData_ReturnsOk()
        {
            // Arrange
            var badge = new BadgeDto
            {
                Name = "Test Badge",
                Description = "This is a test badge",
                Picture = "base64encodedstring"
            };

            var content = new StringContent(JsonConvert.SerializeObject(badge), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("api/v1/badge", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task PutBadge_ValidData_ReturnsOk()
        {
            // Arrange
            var badge = new BadgeDto
            {
                Id = 1, // Assuming this ID exists
                Name = "Updated Badge",
                Description = "This is an updated badge",
                Picture = "updatedbase64encodedstring"
            };

            var content = new StringContent(JsonConvert.SerializeObject(badge), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync("api/v1/badge", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task GetBadges_ReturnsOk()
        {
            // Act
            var response = await _client.GetAsync("api/v1/badges");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task DeleteBadge_ValidId_ReturnsOk()
        {
            // Arrange
            int badgeId = 1; // Assuming this ID exists

            // Act
            var response = await _client.DeleteAsync($"api/v1/badge/{badgeId}");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}