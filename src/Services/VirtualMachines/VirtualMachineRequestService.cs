using Domain.VirtualMachines;
using Shared.VirtualMachines;
using Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Domain.Users;
using Shared.Clients;

namespace Services.VirtualMachines;

public class VirtualMachineRequestService : IVirtualMachineRequestService
{
    private readonly VicDbContext dbContext;

    public VirtualMachineRequestService(VicDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<VirtualMachineRequestDto.Detail> Get(int id)
    {
        var request = await dbContext.VirtualMachineRequests.Include(c => c.Client).FirstOrDefaultAsync(v => v.Id == id);

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
            ClientInfo = request.ClientInfo,
        };
    }

    public async Task<List<VirtualMachineRequestDto.Index>> GetAll()
    {
        return await dbContext.VirtualMachineRequests.Include(x => x.Client)
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
        Client? client = null;
        if(model.Client!=null)
            client = await dbContext.Clients.FirstOrDefaultAsync(x => x.Id == model.Client.Id);
        var vm = await dbContext.VirtualMachines.FirstOrDefaultAsync(x => x.Id == model.VirtualMachineId);

        var request = new VirtualMachineRequest(
            model.StartDate!,
            model.EndDate!,
            model.Reason!,
            model.ProjectName!,
            client!,
            vm!,
            Domain.VirtualMachines.ERequestStatus.Requested!,
            model.ClientInfo!
        );

        if(client is not null)
            client.AddRequest(request);

        dbContext.VirtualMachineRequests.Add(request);
        await dbContext.SaveChangesAsync();

        return request.Id;
    }

    public async Task EditAsync(int id, VirtualMachineRequestDto.Detail request)
    {
        var r = await dbContext.VirtualMachineRequests.SingleOrDefaultAsync(v => v.Id == id);
        var vm = await dbContext.VirtualMachines.FirstOrDefaultAsync(v => v.Id == id);

        if(vm is null)
            throw new EntityNotFoundException(nameof(VirtualMachine), id);
        if (r is null)
            throw new EntityNotFoundException(nameof(VirtualMachineRequest), id);

        r.Status = (Domain.VirtualMachines.ERequestStatus)request.Status;
        r.VirtualMachine = vm;

        await dbContext.SaveChangesAsync();
    }

}
