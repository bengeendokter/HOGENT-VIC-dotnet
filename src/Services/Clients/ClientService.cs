using Domain.Clients;
using Shared.Clients;
using Persistence;
using Microsoft.EntityFrameworkCore;
using Domain.Clients.Users;
using Shared;

namespace Services.Clients;

public class ClientService : IClientService
{
    private readonly VicDbContext dbContext;

    public ClientService(VicDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<List<ClientDto.Index>> GetIndexAsync()
    {
        return await dbContext.Users
            .Select(
                v =>
                    new ClientDto.Index
                    {
                        Id = v.Id,
                        Name = v.Name,
                        PhoneNumber = (v is Client)
                        ClientType = (v is Client) ? (EClientType)((Client)v).Type : EClientType.Internal,
                        ClientOrganisation = (v is Client) ? ((Client)v).ClientOrganisation : null,
                        Role = v.Role,

                    }
            )
            .ToListAsync();
    }

    /*
    public async Task<VirtualMachineDto.Detail> GetDetailAsync(int virtualMachineId)
    {
        var vm = await dbContext.VirtualMachines.FirstOrDefaultAsync(v => v.Id == virtualMachineId);

        if (vm is null)
            throw new EntityNotFoundException(nameof(VirtualMachine), virtualMachineId);

        return new VirtualMachineDto.Detail
        {
            Id = vm.Id,
            Name = vm.Name,
            CPU = vm.CPU,
            RAM = vm.RAM,
            Storage = vm.Storage,
            StartDate = vm.StartDate,
            EndDate = vm.EndDate,
            IsActive = vm.IsActive,
            HostName = vm.HostName,
            FQDN = vm.FQDN,
            Availability = (Shared.VirtualMachines.EDay)vm.Availability,
            BackupFrequency = (Shared.VirtualMachines.EBackupFrequency)vm.BackupFrequency,
            IsHighlyAvailable = vm.IsHighlyAvailable,
            Mode = (Shared.VirtualMachines.EMode)vm.Mode,
            Template = (Shared.VirtualMachines.ETemplate)vm.Template
        };
    }

    public async Task<int> CreateAsync(VirtualMachineDto.Mutate model)
    {
        if (await dbContext.VirtualMachines.AnyAsync(v => v.Name == model.Name))
            throw new EntityAlreadyExistsException(
                nameof(VirtualMachine),
                nameof(VirtualMachine.Name),
                model.Name
            );

        var vm = new VirtualMachine(
            // model.Client!,
            model.Name!,
            model.HostName!,
            model.StartDate,
            model.EndDate,
            model.FQDN!,
            model.Poorten!,
            (Domain.VirtualMachines.ETemplate)model.Template,
            model.Host!,
            model.CPU,
            model.RAM,
            model.Storage,
            (Domain.VirtualMachines.EMode)model.Mode,
            (Domain.VirtualMachines.EBackupFrequency)model.BackupFrequency,
            (Domain.VirtualMachines.EDay)model.Availability,
            model.IsHighlyAvailable,
            false
        );

        dbContext.VirtualMachines.Add(vm);
        await dbContext.SaveChangesAsync();

        return vm.Id;
    }

    public async Task EditAsync(int virtualMachineId, VirtualMachineDto.Mutate model)
    {
        var vm = await dbContext.VirtualMachines.SingleOrDefaultAsync(
            v => v.Id == virtualMachineId
        );

        if (vm is null)
            throw new EntityNotFoundException(nameof(VirtualMachine), virtualMachineId);

        vm.Name = model.Name!;
        vm.CPU = model.CPU;
        vm.RAM = model.RAM;
        vm.Storage = model.Storage;
        vm.StartDate = model.StartDate;
        vm.EndDate = model.EndDate;
        vm.IsActive = model.IsActive;
        vm.HostName = model.HostName!;
        vm.FQDN = model.FQDN!;
        vm.IsHighlyAvailable = model.IsHighlyAvailable;
        vm.Template = (Domain.VirtualMachines.ETemplate)model.Template!;
        vm.BackupFrequency = (Domain.VirtualMachines.EBackupFrequency)model.BackupFrequency;
        vm.Availability = (Domain.VirtualMachines.EDay)model.Availability;
        vm.Mode = (Domain.VirtualMachines.EMode)model.Mode;
        vm.Host = model.Host!;
        // vm.Client = model.Client!;
        vm.Poorten = model.Poorten!;

        await dbContext.SaveChangesAsync();
    }*/

    public List<ClientDto.Index> GetAll(ClientRequest.Index request)
    {
        throw new NotImplementedException();
    }

    public ClientDto.Detail GetDetail(int clientId)
    {
        throw new NotImplementedException();
    }

    public int Create(ClientDto.Mutate model)
    {
        throw new NotImplementedException();
    }

    public void Edit(int clientId, ClientDto.Mutate model)
    {
        throw new NotImplementedException();
    }

    public void Delete(int clientId)
    {
        throw new NotImplementedException();
    }
    
}
