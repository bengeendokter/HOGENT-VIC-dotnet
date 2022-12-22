using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Shared.Clients;
using Domain.Users;
using EClientType = Domain.Users.EClientType;
using Shared.Users;
using System.Security.Claims;
using Shared.Activities;
using Shared.AuthUsers;
using Shared.VirtualMachines;

namespace Services.Clients;

public class AuthUserService
{
    private readonly VicDbContext dbContext;
    private readonly List<string> roles = new() { "User", "Moderator", "Admin" };
    private readonly ClaimsPrincipal claimsPrincipal = default!;

    public AuthUserService(VicDbContext dbContext, ClaimsPrincipal claimsPrincipal)
    {
        this.dbContext = dbContext;
        this.claimsPrincipal = claimsPrincipal;
    }
    public async Task<List<VirtualMachineDto.Index>> GetMyVirtualMachines(VirtualMachineReq.Index request)
    {
        var email = claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value;
        //var id = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var query = dbContext.VirtualMachines.Include(x => x.Client).AsQueryable();

        query = query.Where(x => x.Client.Email == email);

        if (!string.IsNullOrWhiteSpace(request.Searchterm))
        {
            query = query.Where(x => 
            x.Name.Contains(request.Searchterm, StringComparison.OrdinalIgnoreCase) || 
            x.Template.ToString().Contains(request.Searchterm, StringComparison.OrdinalIgnoreCase)
            );
        }

        var items = await query
           .OrderByDescending(x => x.CreatedAt)
           .Skip((request.Page - 1) * request.PageSize)
           .Take(request.PageSize)
            .Select(
                v =>
                    new VirtualMachineDto.Index
                    {
                        Id = v.Id,
                        Name = v.Name,
                        CPU = v.CPU,
                        RAM = v.RAM,
                        Storage = v.Storage,
                        StartDate = v.StartDate,
                        EndDate = v.EndDate,
                        IsActive = v.IsActive,
                        Template = (Shared.VirtualMachines.ETemplate)v.Template,
                        IsHighlyAvailable = v.IsHighlyAvailable,
                        BackupFrequency = (Shared.VirtualMachines.EBackupFrequency)v.BackupFrequency,
                        Client = (v.Client != null) ? new ClientDto.Index()
                        {
                            Id = v.Client.Id,
                            Name = v.Client.Name,
                            Surname = v.Client.Surname,
                            ClientOrganisation = v.Client.ClientOrganisation,
                            ClientType = (Shared.Clients.EClientType)v.Client.ClientType,
                            PhoneNumber = v.Client.PhoneNumber
                        } :
                            null
                    }
            )
            .ToListAsync();

        return items;
    }
}
