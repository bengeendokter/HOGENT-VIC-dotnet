using Domain.VirtualMachines;
using Shared.VirtualMachines;
using Persistence;
using Microsoft.EntityFrameworkCore;

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
                        IsActive = v.IsActive
                    }
            )
            .ToListAsync();
    }

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
    }
}
