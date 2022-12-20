using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Auth0.ManagementApi.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.AuthUsers;
using static Shared.AuthUsers.AuthUserDto.Mutate;
using Role = Auth0.ManagementApi.Models.Role;

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

        [HttpPost("wijzig/rollen/{userId}")]
        public async Task<AuthUserResponse.Create.Role> WijzigRoles(string userId, [FromBody] AuthUserRequest.Roles request)
        {
            var allRoles = await _managementApiClient.Roles.GetAllAsync(new GetRolesRequest());

            var adminRole = allRoles.First(x => x.Name == "Administrator");
            var modRole = allRoles.First(x => x.Name == "Moderator");
            var customerRole = allRoles.First(x => x.Name == "Customer");
            var userRole = allRoles.First(x => x.Name == "User");

            List<Role> ToBeAssignedRoles = new();
            List<Role> ToBeDeletedRoles = new();

            if (request.IsAdministrator)
            {
                ToBeAssignedRoles.Add(adminRole);
            } else
            {
                ToBeDeletedRoles.Add(adminRole);
            }

            if (request.IsModerator)
            {
                ToBeAssignedRoles.Add(modRole);
            }
            else
            {
                ToBeDeletedRoles.Add(modRole);
            }

            if (request.IsCustomer)
            {
                ToBeAssignedRoles.Add(customerRole);
            }
            else
            {
                ToBeDeletedRoles.Add(customerRole);
            }

            if (request.IsUser)
            {
                ToBeAssignedRoles.Add(userRole);
            }
            else
            {
                ToBeDeletedRoles.Add(userRole);
            }


            var assignRoleRequest = new AssignRolesRequest
            {
                Roles = ToBeAssignedRoles.Select(x => x.Id).ToArray()
            };

            if (ToBeAssignedRoles.Count >= 1)
            {
                await _managementApiClient.Users.AssignRolesAsync(userId, assignRoleRequest);
            }

            assignRoleRequest.Roles = ToBeDeletedRoles.Select(x => x.Id).ToArray();

            if (ToBeDeletedRoles.Count >= 1)
            {
                await _managementApiClient.Users.RemoveRolesAsync(userId, assignRoleRequest);
            }

            var response =  await _managementApiClient.Users.GetRolesAsync(userId);
            var rollen = response.Select(x => new AuthUserDto.Detail.UserRole()
            {
                Id = x.Id,
                Role = x.Name
            });

            return new AuthUserResponse.Create.Role()
            {
                rollen = rollen
            };
        }
    }
}