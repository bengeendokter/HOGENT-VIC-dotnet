using Domain.VirtualMachines;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Shared.Clients;
using Shared.VirtualMachines;
using System.Security.Claims;
using ERequestStatus = Domain.VirtualMachines.ERequestStatus;

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
        var query = dbContext.VirtualMachines.AsQueryable();

        query = query.Where(x => x.Client.Email == email);

        if (!string.IsNullOrWhiteSpace(request.Searchterm))
        {
            query = query.Where(x =>
            x.Name.Contains(request.Searchterm)
            );
        }

        if (!string.IsNullOrWhiteSpace(request.Status))
        {
            var status = request.Status;
            query = query.Where(x => (x.IsActive ? "Aan" : "Uit") == status);
        }

        if (!string.IsNullOrWhiteSpace(request.HoogBeschikbaar))
        {
            var hoogB = request.HoogBeschikbaar;
            query = query.Where(x => (x.IsHighlyAvailable ? "Ja" : "Nee") == hoogB);
        }

        if (!string.IsNullOrWhiteSpace(request.SortBy))
        {
            query = SortRequestQueryVM(request.SortBy, query);
        }
        else
        {
            query = query.OrderByDescending(x => x.UpdatedAt);
        }

        var items = await query
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

    public async Task<List<VirtualMachineRequestDto.Index>> GetMyRequests(VirtualMachineRequestReq.Index request)
    {
        var email = claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value;
        //var id = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var query = dbContext.VirtualMachineRequests.Include(x => x.Client).AsQueryable();

        query = query.Where(x => x.Client.Email == email);

        if (!string.IsNullOrWhiteSpace(request.Searchterm))
        {
            query = query.Where(x =>
            x.ProjectName.Contains(request.Searchterm)
            );
        }

        if (!string.IsNullOrWhiteSpace(request.Status))
        {
            var status = request.Status;
            query = query.Where(x =>
                ((x.Status == ERequestStatus.Denied) ? "Geweigerd" :
                ((x.Status == ERequestStatus.Requested) ? "Aangevraagd" :
                ((x.Status == ERequestStatus.Handled) ? "Behandeld" : "Aanvaard"))) == status
                );
        }
        if (!string.IsNullOrWhiteSpace(request.SortBy))
        {
            query = SortRequestQueryRequest(request.SortBy, query);
        }
        else
        {
            query = query.OrderByDescending(x => x.UpdatedAt);
        }

        var items = await query
        .Skip((request.Page - 1) * request.PageSize)
        .Take(request.PageSize)
        .Select(
            r =>
                new VirtualMachineRequestDto.Index
                {
                    Id = r.Id,
                    Date = r.CreatedAt,
                    ProjectName = r.ProjectName,
                    StartDate = r.StartDate,
                    Status = (Shared.VirtualMachines.ERequestStatus)r.Status,
                    Client = r.Client != null ? new ClientDto.Index
                    {
                        Id = r.Client.Id,
                        Name = r.Client.Name,
                        Surname = r.Client.Surname,
                        PhoneNumber = r.Client.PhoneNumber,
                        ClientType = (Shared.Clients.EClientType)r.Client.ClientType,
                        ClientOrganisation = r.Client.ClientOrganisation
                    } : null,
                    VirtualMachineId = r.VirtualMachine != null ? r.VirtualMachine.Id : null
                }
        )
        .ToListAsync();

        return items;
    }


    private string GiveStatusAsString(ERequestStatus status)
    {
        return (status) switch
        {
            ERequestStatus.Accepted => "Aanvaard",
            ERequestStatus.Requested => "Aangevraagd",
            ERequestStatus.Handled => "Behandeld",
            ERequestStatus.Denied => "Geweigerd",
            _ => "Onbekend"
        };
    }

    private IQueryable<VirtualMachine> SortRequestQueryVM(string? sortby, IQueryable<VirtualMachine> query)
    {
        return (sortby) switch
        {
            "name" => query.OrderBy(x => x.Name),
            "nameDesc" => query.OrderByDescending(x => x.Name),
            "template" => query.OrderBy(x => x.Template),
            "templateDesc" => query.OrderByDescending(x => x.Template),
            "cpu" => query.OrderBy(x => x.CPU),
            "cpuDesc" => query.OrderByDescending(x => x.CPU),
            "ram" => query.OrderBy(x => x.RAM),
            "ramDesc" => query.OrderByDescending(x => x.RAM),
            "storage" => query.OrderBy(x => x.Storage),
            "storageDesc" => query.OrderByDescending(x => x.Storage),
            "startdate" => query.OrderBy(x => x.StartDate),
            "startdateDesc" => query.OrderByDescending(x => x.StartDate),
            "enddate" => query.OrderBy(x => x.EndDate),
            "enddateDesc" => query.OrderByDescending(x => x.EndDate),
            _ => query
        };
    }

    private IQueryable<VirtualMachineRequest> SortRequestQueryRequest(string? sortby, IQueryable<VirtualMachineRequest> query)
    {
        return (sortby) switch
        {
            "date" => query.OrderBy(x => x.CreatedAt),
            "dateDesc" => query.OrderByDescending(x => x.CreatedAt),
            "project" => query.OrderBy(x => x.ProjectName),
            "projectDesc" => query.OrderByDescending(x => x.ProjectName),
            _ => query
        };
    }
}
