using Shared.Activities;
using System.Net.Http.Json;

namespace Client.Analytics;

public class ActivityService : IActivityService
{
    private readonly HttpClient client;
    private const string endpoint = "api/activity";

    public ActivityService(HttpClient client)
    {
        this.client = client;
    }

    public async  Task<List<ActivityDto.Index>> GetIndexAsync(ActivityRequest.Index request)
    {
        var response = await client.GetFromJsonAsync<List<ActivityDto.Index>>($"{endpoint}?{request.GetQueryString()}");
        return response;
    }
}