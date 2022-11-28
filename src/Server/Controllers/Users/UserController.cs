using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Shared.Clients;
using Shared.Users;

namespace Server.Controllers.Users;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService userService;

    public UserController(IUserService userService)
    {
        this.userService = userService;
    }
    
    [SwaggerOperation("Returns a list of users.")]
    [HttpGet]
    public async Task<UserResult.Index> GetIndex([FromQuery] UserRequest.Index request)
    {
        return await userService.GetIndexAsync(request);
    }
    
    [SwaggerOperation("Returns a specific user by id.")]
    [HttpGet("{userId}")]
    public async Task<UserDto.Detail> GetDetail(int userId)
    {
        return await userService.GetDetailAsync(userId);
    }

    [SwaggerOperation("Creates a new user.")]
    [HttpPost]
    public async Task<IActionResult> Create(UserDto.Mutate model)
    {
        var userId = await userService.CreateAsync(model);
        return CreatedAtAction(nameof(Create), userId);
    }

    
    [SwaggerOperation("Edites an existing user.")]
    [HttpPut("{userId}")]
    public async Task<IActionResult> Edit(int userId, UserDto.Mutate model)
    {
        await userService.EditAsync(userId, model);
        return NoContent();
    }

    [SwaggerOperation("Deletes an existing user.")]
    [HttpDelete("{userId}")]
    public async Task<IActionResult> Delete(int userId)
    {
        await userService.DeleteAsync(userId);
        return NoContent();
    }
}
