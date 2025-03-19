using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestApiNUnitTests.Models;

namespace RestApiNUnitTests.ApiClients
{
    public class ActivityApiClient : ApiClient
    {
        public ActivityApiClient(HttpClient client) : base(client)
        {
        }

        public async Task<Activity[]> GetAllActivitiesAsync()
        {
            var response = await Client.GetAsync("/api/v1/Activities");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Activity[]>(content);
        }

        public async Task<Activity> GetActivityByIdAsync(int id)
        {
            var response = await Client.GetAsync($"/api/v1/Activities/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Activity>(content);
        }

        public async Task<Activity> CreateActivityAsync(Activity activity)
        {
            var json = JsonConvert.SerializeObject(activity);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await Client.PostAsync("/api/v1/Activities", content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Activity>(responseContent);
        }

        public async Task<Activity> UpdateActivityAsync(int id, Activity activity)
        {
            var json = JsonConvert.SerializeObject(activity);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await Client.PutAsync($"/api/v1/Activities/{id}", content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Activity>(responseContent);
        }

        public async Task DeleteActivityAsync(int id)
        {
            var response = await Client.DeleteAsync($"/api/v1/Activities/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}