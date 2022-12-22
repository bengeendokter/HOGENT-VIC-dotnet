using Domain.VirtualMachines;
using Domain.Activities;
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

    public async Task<List<VirtualMachineDto.Index>> GetIndexAsync(VirtualMachineReq.Index request)
    {
        var query = dbContext.VirtualMachines.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Searchterm))
        {
            query = query.Where(x => 
                x.Name.Contains(request.Searchterm, StringComparison.OrdinalIgnoreCase) || 
                x.Client.Surname.Contains(request.Searchterm, StringComparison.OrdinalIgnoreCase) ||
                x.Client.Name.Contains(request.Searchterm, StringComparison.OrdinalIgnoreCase) ||
                x.Template.ToString().Contains(request.Searchterm, StringComparison.OrdinalIgnoreCase)
            );
        }

        if (!string.IsNullOrEmpty(request.Status))
        {
            var status = request.Status == "on";
            query = query.Where(x => x.IsActive == status);
        }

        if (!string.IsNullOrWhiteSpace(request.SortBy))
        {
            query = SortRequestQuery(request.SortBy, query);
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

    public async Task<VirtualMachineDto.Detail> GetDetailAsync(int virtualMachineId)
    {
        var vm = await dbContext.VirtualMachines.FirstOrDefaultAsync(v => v.Id == virtualMachineId);

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
            Software = (Shared.VirtualMachines.ESoftware)vm.Software,
            Template = (Shared.VirtualMachines.ETemplate)vm.Template,
            Poorten = vm.Poorten,
            Client = vmClient is not null ? client : null,
            Host = vm.Host
    };
}

    public async Task<int> CreateAsync(VirtualMachineDto.Mutate model)
    {
        //if (await dbContext.VirtualMachines.AnyAsync(v => v.Name == model.Name))
        //    throw new EntityAlreadyExistsException(
        //        nameof(VirtualMachine),
        //        nameof(VirtualMachine.Name),
        //        model.Name
        //    );
        Client? client = null;
        if (model.ClientId != null)
            client = await dbContext.Clients.FirstOrDefaultAsync(x => x.Id == model.ClientId);

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
            (Domain.VirtualMachines.ESoftware)model.Software,
            (Domain.VirtualMachines.EBackupFrequency)model.BackupFrequency,
            (Domain.VirtualMachines.EDay)model.Availability,
            model.IsHighlyAvailable,
            false,
            client!
        );
        dbContext.VirtualMachines.Add(vm);

        var activity = new Activity(EActivity.Added, 
            vm.Name,
            vm.Client is not null ? $"{vm.Client.Surname} {vm.Client.Name}" : "Geen klant",
            vm.CPU, 
            vm.RAM,
            vm.Storage
        );
        dbContext.Activities.Add(activity);

        await dbContext.SaveChangesAsync();

        return vm.Id;
    }

    public async Task EditAsync(int virtualMachineId, VirtualMachineDto.Mutate model)
    {
        var vm = await dbContext.VirtualMachines.SingleOrDefaultAsync(
            v => v.Id == virtualMachineId
        );

        if (vm is null)
        {
            throw new EntityNotFoundException(nameof(VirtualMachine), virtualMachineId);
        }

        var activity = new Activity(EActivity.Edited, 
            vm.Name, 
            vm.Client is not null ? $"{vm.Client.Surname} {vm.Client.Name}": "Geen klant", 
            model.CPU - vm.CPU, 
            model.RAM - vm.RAM,
            model.Storage - vm.Storage
        );

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
        vm.Software = (Domain.VirtualMachines.ESoftware)model.Software;
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

        dbContext.Activities.Add(activity);

        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int virtualMachineId)
    {
        VirtualMachine? vm = await dbContext.VirtualMachines.SingleOrDefaultAsync(x => x.Id == virtualMachineId);

        if (vm is null)
            throw new EntityNotFoundException(nameof(VirtualMachine), virtualMachineId);

        dbContext.VirtualMachines.Remove(vm);

        var activity = new Activity(EActivity.Deleted, 
            vm.Name,
            vm.Client is not null ? $"{vm.Client.Surname} {vm.Client.Name}" : "Geen klant",
            -vm.CPU,
            -vm.RAM,
            -vm.Storage
        );
        dbContext.Activities.Add(activity);

        await dbContext.SaveChangesAsync();
    }

    private IQueryable<VirtualMachine> SortRequestQuery(string? sortby, IQueryable<VirtualMachine> query)
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
            "client" => query.OrderBy(x => x.Client.Name),
            "clientDesc" => query.OrderByDescending(x => x.Client.Name),
            "backup" => query.OrderBy(x => x.BackupFrequency),
            "backupDesc" => query.OrderByDescending(x => x.BackupFrequency),
            "highav" => query.OrderBy(x => x.IsHighlyAvailable),
            "highavDesc" => query.OrderByDescending(x => x.IsHighlyAvailable),
            _ => query
        };
    }
}
