using Domain.Users;
using Domain.VirtualMachines;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Shared.Clients;
using Shared.VirtualMachines;
using System.Security.Claims;

namespace Services.VirtualMachines;

public class VirtualMachineRequestService : IVirtualMachineRequestService
{
    private readonly VicDbContext dbContext;
    private readonly ClaimsPrincipal claimsPrincipal = default!;

    public VirtualMachineRequestService(VicDbContext dbContext, ClaimsPrincipal claimsPrincipal)
    {
        this.dbContext = dbContext;
        this.claimsPrincipal = claimsPrincipal;
    }
    public async Task<VirtualMachineRequestDto.Detail> Get(int id)
    {
        var request = await dbContext.VirtualMachineRequests.FirstOrDefaultAsync(v => v.Id == id);

        if (request is null)
            throw new EntityNotFoundException(nameof(VirtualMachine), id);

        return new VirtualMachineRequestDto.Detail
        {
            Id = request.Id,
            Date = request.CreatedAt,
            ProjectName = request.ProjectName,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Reason = request.Reason,
            Status = (Shared.VirtualMachines.ERequestStatus)request.Status,
            Client = request.Client != null ? new ClientDto.Index
            {
                Id = request.Client.Id,
                Name = request.Client.Name,
                Surname = request.Client.Surname,
                PhoneNumber = request.Client.PhoneNumber,
                ClientType = (Shared.Clients.EClientType)request.Client.ClientType,
                ClientOrganisation = request.Client.ClientOrganisation
            } : null,
            VirtualMachineId = request.VirtualMachine != null ? request.VirtualMachine.Id : null,
        };
    }

    public async Task<List<VirtualMachineRequestDto.Index>> GetAll(VirtualMachineRequestReq.Index request)
    {
        var query = dbContext.VirtualMachineRequests.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Status))
        {
            var status = Enum.Parse<Domain.VirtualMachines.ERequestStatus>(request.Status, true);
            query = query.Where(x => x.Status.Equals(status));
        }
        if (!string.IsNullOrWhiteSpace(request.SortBy))
        {
            query = SortRequestQuery(request.SortBy, query);
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

    public async Task<List<VirtualMachineRequestDto.Index>> GetRequestsFromClient(int clientId)
    {
        var client = await dbContext.Clients.FirstOrDefaultAsync(x => x.Id == clientId);

        if (client is null)
            throw new EntityNotFoundException(nameof(Client), clientId);

        var requests = client.Requests;
        if (requests.Any())
        {
            return requests
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
                ).ToList();
        }
        else return new List<VirtualMachineRequestDto.Index>();
    }


    public async Task<int> CreateAsync(VirtualMachineRequestDto.Create model)
    {

        var email = claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value;


        Client? client = null;
        if (email != null)
        {
            client = await dbContext.Clients.FirstOrDefaultAsync(x => x.Email == email);
        }
        var vm = await dbContext.VirtualMachines.FirstOrDefaultAsync(x => x.Id == model.VirtualMachineId);

        var request = new VirtualMachineRequest(
            model.StartDate.ToUniversalTime()!,
            model.EndDate.ToUniversalTime()!,
            model.Reason!,
            model.ProjectName!,
            client!,
            vm!,
            Domain.VirtualMachines.ERequestStatus.Requested!
        );

        if (client is not null)
            client.AddRequest(request);

        dbContext.VirtualMachineRequests.Add(request);
        await dbContext.SaveChangesAsync();

        return request.Id;
    }

    public async Task EditAsync(int id, VirtualMachineRequestDto.Detail request)
    {
        var r = await dbContext.VirtualMachineRequests.SingleOrDefaultAsync(v => v.Id == id);
        var vm = await dbContext.VirtualMachines.FirstOrDefaultAsync(v => v.Id == id);

        if (vm is null)
            throw new EntityNotFoundException(nameof(VirtualMachine), id);
        if (r is null)
            throw new EntityNotFoundException(nameof(Domain.VirtualMachines.VirtualMachineRequest), id);

        r.Status = (Domain.VirtualMachines.ERequestStatus)request.Status;
        r.VirtualMachine = vm;

        await dbContext.SaveChangesAsync();
    }

    private IQueryable<VirtualMachineRequest> SortRequestQuery(string? sortby, IQueryable<VirtualMachineRequest> query)
    {
        return (sortby) switch
        {
            "date" => query.OrderBy(x => x.UpdatedAt),
            "dateDesc" => query.OrderByDescending(x => x.UpdatedAt),
            "project" => query.OrderBy(x => x.ProjectName),
            "projectDesc" => query.OrderByDescending(x => x.ProjectName),
            _ => query
        };
    }
}
