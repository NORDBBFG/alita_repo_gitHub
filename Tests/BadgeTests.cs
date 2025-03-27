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
        public async Task PostBadge_ShouldAddNewBadge()
        {
            // Arrange
            var newBadge = new
            {
                name = "Test Badge",
                description = "This is a test badge",
                picture = "base64EncodedString"
            };
            var content = new StringContent(JsonConvert.SerializeObject(newBadge), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync($"{BaseUrl}/api/v1/badge", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.IsNotEmpty(responseContent);
        }

        [Test]
        public async Task PutBadge_ShouldEditExistingBadge()
        {
            // Arrange
            var updatedBadge = new
            {
                id = 1, // Assume this badge exists
                name = "Updated Test Badge",
                description = "This is an updated test badge",
                picture = "updatedBase64EncodedString"
            };
            var content = new StringContent(JsonConvert.SerializeObject(updatedBadge), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync($"{BaseUrl}/api/v1/badge", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.IsNotEmpty(responseContent);
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
        }
    }
}
