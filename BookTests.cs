using NUnit.Framework;
using FluentAssertions;
using Newtonsoft.Json;
using RestApiNUnitTests.Models;
using RestApiNUnitTests.ApiClients;

namespace RestApiNUnitTests
{
    [TestFixture]
    public class BookTests
    {
        private BookApiClient _bookApiClient;

        [SetUp]
        public void SetUp()
        {
            _bookApiClient = new BookApiClient();
        }

        [Test]
        public async Task CreateBook_ShouldReturnSuccess_AndValidateResponse()
        {
            var newBook = new Book
            {
                Id = 22,
                Title = "Test Automation with RestSharp",
                Description = "A book about API testing",
                PageCount = 200,
                Excerpt = "This is an excerpt",
                PublishDate = DateTime.UtcNow
            };

            var response = await _bookApiClient.CreateBookAsync(newBook);
            var responseBody = JsonConvert.DeserializeObject<Book>(response.Content);

            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK)); 
            responseBody.Should().NotBeNull();
            responseBody.Title.Should().Be(newBook.Title); 
            responseBody.Description.Should().Be(newBook.Description); 
            responseBody.PageCount.Should().Be(newBook.PageCount); 
        }
    }
}
