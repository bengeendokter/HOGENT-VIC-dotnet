using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Auth0.ManagementApi.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.AuthUsers;
using static Shared.AuthUsers.AuthUserDto.Mutate;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Administrator, Moderator")]

    public class AuthUserController : ControllerBase
    {
        private readonly IManagementApiClient _managementApiClient;

        public AuthUserController(IManagementApiClient managementApiClient)
        {
            _managementApiClient = managementApiClient;
        }

        [HttpGet]
        public async Task<IEnumerable<AuthUserDto.Index>> GetUsers()
        {
            var users = await _managementApiClient.Users.GetAllAsync(new GetUsersRequest(), new PaginationInfo());
            return users.Select(x => new AuthUserDto.Index
            {
                Id = x.UserId,
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Blocked = x.Blocked ?? false,
            });
        }
        [HttpGet("{userId}")]
        public async Task<AuthUserDto.Detail.General> GetUser(string userId)
        {
            var user = await _managementApiClient.Users.GetAsync(userId);
            return new AuthUserDto.Detail.General()
            {
                Id = userId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Blocked = user.Blocked ?? false,
            };
        }
        [HttpGet("rol/{userId}")]
        public async Task<IEnumerable<AuthUserDto.Detail.UserRole>> GetRolesFromUser(string userId)
        {
            var roles = await _managementApiClient.Users.GetRolesAsync(userId, new PaginationInfo());
            return roles.Select(x => new AuthUserDto.Detail.UserRole()
            {
                Id = x.Id,
                Role = x.Name
            });
        }

        [HttpGet("roles")]
        public async Task<IEnumerable<AuthUserDto.Detail.UserRole>> GetAllRoles()
        {
            var roles = await _managementApiClient.Roles.GetAllAsync(new GetRolesRequest());
            return roles.Select(x => new AuthUserDto.Detail.UserRole()
            {
                Id = x.Id,
                Role = x.Name
            });
        }

        [HttpGet("verwijder_rol/{userId}")]
        public async Task<IActionResult> DeleteRoles(string userId, [FromBody] AuthUserRequest.Roles request)
        {
            await _managementApiClient.Users.RemoveRolesAsync(userId, new AssignRolesRequest() { Roles = request.roles });
            return NoContent();
        }
    }
}