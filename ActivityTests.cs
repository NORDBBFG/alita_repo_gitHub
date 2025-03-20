using NUnit.Framework;
using System;
using System.Threading.Tasks;
using RestApiNUnitTests.ApiClients;
using RestApiNUnitTests.Models;
using System.Collections.Generic;

namespace RestApiNUnitTests
{
    [TestFixture]
    public class ActivityTests
    {
        private ActivityApiClient _client;

        [SetUp]
        public void Setup()
        {
            _client = new ActivityApiClient("https://fakerestapi.azurewebsites.net");
        }

        [Test]
        public async Task GetAllActivities_ReturnsActivities()
        {
            var activities = await _client.GetAllActivitiesAsync();
            Assert.IsNotNull(activities);
            Assert.IsTrue(activities.Count > 0);
        }

        [Test]
        public async Task GetActivityById_ReturnsCorrectActivity()
        {
            int id = 1;
            var activity = await _client.GetActivityByIdAsync(id);
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

            var createdActivity = await _client.CreateActivityAsync(newActivity);
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

            var result = await _client.UpdateActivityAsync(id, updatedActivity);
            Assert.IsNotNull(result);
            Assert.AreEqual(updatedActivity.Title, result.Title);
            Assert.AreEqual(updatedActivity.DueDate.Date, result.DueDate.Date);
            Assert.AreEqual(updatedActivity.Completed, result.Completed);
        }

        [Test]
        public async Task DeleteActivity_DoesNotThrowException()
        {
            int id = 1;
            Assert.DoesNotThrowAsync(() => _client.DeleteActivityAsync(id));
        }
    }
}
