using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DonorApp.Services.DTO;

namespace Tests
{
    [TestFixture]
    public class BadgeTests
    {
        private HttpClient _client;
        private const string BaseUrl = "http://example.com/api/v1";

        [SetUp]
        public void Setup()
        {
            _client = new HttpClient();
        }

        [Test]
        public async Task PostBadge_ShouldAddNewBadge()
        {
            // Arrange
            var badge = new BadgeDto
            {
                Name = "Test Badge",
                Description = "This is a test badge",
                Picture = "test_picture.jpg"
            };

            var content = new StringContent(JsonConvert.SerializeObject(badge), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync($"{BaseUrl}/badge", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task PutBadge_ShouldEditBadge()
        {
            // Arrange
            var badge = new BadgeDto
            {
                Id = 1,
                Name = "Updated Test Badge",
                Description = "This is an updated test badge",
                Picture = "updated_test_picture.jpg"
            };

            var content = new StringContent(JsonConvert.SerializeObject(badge), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync($"{BaseUrl}/badge", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task GetBadges_ShouldReturnListOfBadges()
        {
            // Act
            var response = await _client.GetAsync($"{BaseUrl}/badges");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            var badges = JsonConvert.DeserializeObject<BadgeDto[]>(content);
            Assert.IsNotNull(badges);
            Assert.IsNotEmpty(badges);
        }

        [Test]
        public async Task DeleteBadge_ShouldRemoveBadge()
        {
            // Arrange
            int badgeId = 1;

            // Act
            var response = await _client.DeleteAsync($"{BaseUrl}/badge/{badgeId}");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}