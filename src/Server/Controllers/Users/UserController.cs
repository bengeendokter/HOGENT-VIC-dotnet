using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Shared.Clients;
using Shared.Users;
using Microsoft.AspNetCore.Authorization;
using Services.Clients;

namespace Server.Controllers.Users;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService userService;

    public UserController(IUserService userService)
    {
        this.userService = userService;
    }

    [Authorize(Roles = "Administrator, Moderator")]
    [SwaggerOperation("Returns a list of users.")]
    [HttpGet]
    public async Task<UserResult.Index> GetIndex([FromQuery] UserRequest.Index request)
    {
        return await userService.GetIndexAsync(request);
    }

    [Authorize(Roles = "Administrator, Moderator")]
    [SwaggerOperation("Returns a specific user by id.")]
    [HttpGet("{userId}")]
    public async Task<UserDto.Detail> GetDetail(int userId)
    {
        return await userService.GetDetailAsync(userId);
    }

    [Authorize(Roles = "Administrator, Moderator")]
    [SwaggerOperation("Creates a new user.")]
    [HttpPost]
    public async Task<IActionResult> Create(UserDto.Mutate model)
    {
        var userId = await userService.CreateAsync(model);
        return CreatedAtAction(nameof(Create), userId);
    }

    [Authorize(Roles = "Administrator, Moderator")]
    [SwaggerOperation("Edites an existing user.")]
    [HttpPut("{userId}")]
    public async Task<IActionResult> Edit(int userId, UserDto.Mutate model)
    {
        await userService.EditAsync(userId, model);
        return NoContent();
    }

    [Authorize(Roles = "Administrator, Moderator")]
    [SwaggerOperation("Deletes an existing user.")]
    [HttpDelete("{userId}")]
    public async Task<IActionResult> Delete(int userId)
    {
        await userService.DeleteAsync(userId);
        return NoContent();
    }
}
