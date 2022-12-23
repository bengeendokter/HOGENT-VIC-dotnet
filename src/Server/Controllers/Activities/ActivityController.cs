using Microsoft.AspNetCore.Mvc;
using Shared.Activities;
using Swashbuckle.AspNetCore.Annotations;

namespace Server.Controllers.Activities;

[ApiController]
[Route("api/[controller]")]
// [Authorize(Roles = "Moderator, Administrator")]
public class ActivityController : ControllerBase
{
    private readonly IActivityService activityService;

    public ActivityController(IActivityService activityService)
    {
        this.activityService = activityService;
    }

    // [Authorize(Roles = "Administrator, Moderator")]
    [SwaggerOperation("Returns a list of activities.")]
    [HttpGet]
    public async Task<List<ActivityDto.Index>> GetIndex([FromQuery] ActivityRequest.Index request)
    {
        return await activityService.GetIndexAsync(request);
    }
}
