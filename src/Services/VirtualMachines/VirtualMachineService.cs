using Domain.VirtualMachines;
using Shared.VirtualMachines;
using Persistence;
using Microsoft.EntityFrameworkCore;
using Domain.Users;
using Shared.Clients;

namespace Services.VirtualMachines;

public class VirtualMachineService : IVirtualMachineService
{
    private readonly VicDbContext dbContext;

    public VirtualMachineService(VicDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<List<VirtualMachineDto.Index>> GetIndexAsync()
    {
        return await dbContext.VirtualMachines
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
                        Client = (v.Client != null) ? new ClientDto.Index() {
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
    }

    public async Task<VirtualMachineDto.Detail> GetDetailAsync(int virtualMachineId)
    {
        var vm = await dbContext.VirtualMachines.Include(x => x.Client).FirstOrDefaultAsync(v => v.Id == virtualMachineId);

        if (vm is null)
            throw new EntityNotFoundException(nameof(VirtualMachine), virtualMachineId);

        var vmClient = vm.Client;

        ClientDto.Index? client = vmClient is null ?
            null
            :
            new ClientDto.Index()
            {
                Id = vmClient.Id,
                ClientOrganisation = vmClient.ClientOrganisation,
                ClientType = (Shared.Clients.EClientType)vmClient.ClientType,
                Name = vmClient.Name,
                Surname = vmClient.Surname,
                PhoneNumber = vmClient.PhoneNumber
            };


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
            Template = (Shared.VirtualMachines.ETemplate)vm.Template,
            Poorten = vm.Poorten,
            Client = vmClient is not null ? client : null,
            Host = vm.Host
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
            false,
            null
            //model.Client!,
        );

        dbContext.VirtualMachines.Add(vm);
        await dbContext.SaveChangesAsync();

        return vm.Id;
    }

    public async Task EditAsync(int virtualMachineId, VirtualMachineDto.Mutate model, int? clientId = -1)
    {
        var vm = await dbContext.VirtualMachines.SingleOrDefaultAsync(
            v => v.Id == virtualMachineId
        );

        if (vm is null)
        {
            throw new EntityNotFoundException(nameof(VirtualMachine), virtualMachineId);
        }

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
        vm.Poorten = model.Poorten!;

        int clientID = model.ClientId;

        if (clientID != 0)
        {
            var client = await dbContext.Clients.SingleOrDefaultAsync(c => c.Id == clientID);

            if (client is null)
            {
                throw new EntityNotFoundException(nameof(Client), clientID);
            }

            client.AddVM(vm);
            vm.Client = client;

        }

        await dbContext.SaveChangesAsync();
    }
}
