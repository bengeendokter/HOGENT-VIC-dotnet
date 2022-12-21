﻿using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Auth0.ManagementApi.Paging;
using Auth0Net.DependencyInjection.Cache;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.AuthUsers;
using static Shared.AuthUsers.AuthUserDto.Mutate;
using Role = Auth0.ManagementApi.Models.Role;
using Microsoft.Extensions.Configuration;
using System.Net;

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
        public async Task<IEnumerable<AuthUserDto.Index>> GetUsers([FromQuery] AuthUserRequest.Index request)
        {
            string searchQueryString = "";

            if (!String.IsNullOrWhiteSpace(request.Searchterm))
            {
                string searchTerm = request.Searchterm;
                if (searchTerm.Length >= 3)
                {
                    searchQueryString += $"email:*{searchTerm}* OR ";
                    searchQueryString += $"name:*{searchTerm}* OR ";
                    searchQueryString += $"family_name:*{searchTerm}* OR ";
                    searchQueryString += $"given_name:*{searchTerm}*";
                }
            }
            var userRequest = new GetUsersRequest()
            {
                Query = searchQueryString
            };

            var users = await _managementApiClient.Users.GetAllAsync(userRequest, new PaginationInfo(request.Page, request.PageSize));
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
                ScreenName = user.FullName
            };
        }

        [HttpPut("{userId}")]
        public async Task<AuthUserDto.Detail.General> UpdateUser(string userId, [FromBody] AuthUserRequest.General request)
        {
            // get Client_id from appsettings.json
            var clientId = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Auth0")["ClientId"];

            // Get possible users using given email
            IEnumerable<User> possibleUser = await _managementApiClient.Users.GetUsersByEmailAsync(request.Email);

            // Check if email is already used
            if (possibleUser.Any() && possibleUser.Where(x => x.UserId == userId).ToList().Count == 0)
            {
                throw new Exception($"Account with email {request.Email} already exists.");
            }


            var updateRequest = new UserUpdateRequest()
            {
                Blocked = request.Blocked,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                FullName = request.ScreenName,
                ClientId = clientId
            };

            // Update user
            var user = await _managementApiClient.Users.UpdateAsync(userId, updateRequest);
            return new AuthUserDto.Detail.General()
            {
                Id = userId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Blocked = user.Blocked ?? false,
                ScreenName = user.FullName
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