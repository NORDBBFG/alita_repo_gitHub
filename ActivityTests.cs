using System;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using RestApiNUnitTests.ApiClients;
using RestApiNUnitTests.Models;

namespace RestApiNUnitTests
{
    [TestFixture]
    public class ActivityTests
    {
        private HttpClient _httpClient;
        private ActivityApiClient _activityApiClient;

        [SetUp]
        public void Setup()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://fakerestapi.azurewebsites.net/");
            _activityApiClient = new ActivityApiClient(_httpClient);
        }

        [Test]
        public async Task GetAllActivities_ReturnsActivities()
        {
            var activities = await _activityApiClient.GetAllActivitiesAsync();
            Assert.IsNotNull(activities);
            Assert.IsTrue(activities.Length > 0);
        }

        [Test]
        public async Task GetActivityById_ReturnsCorrectActivity()
        {
            int id = 1;
            var activity = await _activityApiClient.GetActivityByIdAsync(id);
            Assert.IsNotNull(activity);
            Assert.AreEqual(id, activity.Id);
        }

        [Test]
        public async Task CreateActivity_ReturnsCreatedActivity()
        {
            var newActivity = new Activity
            {
                Title = "Test Activity",
                DueDate = DateTime.Now.AddDays(7),
                Completed = false
            };

            var createdActivity = await _activityApiClient.CreateActivityAsync(newActivity);
            Assert.IsNotNull(createdActivity);
            Assert.AreEqual(newActivity.Title, createdActivity.Title);
            Assert.AreEqual(newActivity.DueDate.Date, createdActivity.DueDate.Date);
            Assert.AreEqual(newActivity.Completed, createdActivity.Completed);
        }

        [Test]
        public async Task UpdateActivity_ReturnsUpdatedActivity()
        {
            int id = 1;
            var updatedActivity = new Activity
            {
                Id = id,
                Title = "Updated Test Activity",
                DueDate = DateTime.Now.AddDays(14),
                Completed = true
            };

            var result = await _activityApiClient.UpdateActivityAsync(id, updatedActivity);
            Assert.IsNotNull(result);
            Assert.AreEqual(updatedActivity.Id, result.Id);
            Assert.AreEqual(updatedActivity.Title, result.Title);
            Assert.AreEqual(updatedActivity.DueDate.Date, result.DueDate.Date);
            Assert.AreEqual(updatedActivity.Completed, result.Completed);
        }

        [Test]
        public async Task DeleteActivity_DoesNotThrowException()
        {
            int id = 1;
            Assert.DoesNotThrowAsync(() => _activityApiClient.DeleteActivityAsync(id));
        }

        [TearDown]
        public void TearDown()
        {
            _httpClient.Dispose();
        }
    }
}