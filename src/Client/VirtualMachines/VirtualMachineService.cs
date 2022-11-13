namespace Client.VirtualMachines;

public class VirtualMachineService : IVirtualMachineService
{
    private readonly List<VirtualMachineDto.Detail> _vms = new();

    public VirtualMachineService()
    {
        SetDummyVirtualMachineList();
    }

    public VirtualMachineDto.Detail? Get(int id)
    {
        var vm = _vms.FirstOrDefault(v => v.Id == id);
        if (vm == null) return null;

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
            Availability = vm.Availability,
            BackupFrequency = vm.BackupFrequency,
            IsHighlyAvailable = vm.IsHighlyAvailable,
            Mode = vm.Mode,
            Template = vm.Template
        };
    }

    public List<VirtualMachineDto.Index> GetAll()
    {
        return _vms.Select(v => new VirtualMachineDto.Index
        {
            Id = v.Id,
            Name = v.Name,
            CPU = v.CPU,
            RAM = v.RAM,
            Storage = v.Storage,
            StartDate = v.StartDate,
            EndDate = v.EndDate,
            IsActive = v.IsActive
        }).ToList();
    }

    public VirtualMachineDto.Detail? Update(int id, VirtualMachineDto.Mutate updatedVm)
    {
        var vm = _vms.FirstOrDefault(v => v.Id == id);
        if (vm == null) return null;

        vm.Name = updatedVm.Name;
        vm.CPU = updatedVm.CPU;
        vm.RAM = updatedVm.RAM;
        vm.Storage = updatedVm.Storage;
        vm.StartDate = updatedVm.StartDate;
        vm.EndDate = updatedVm.EndDate;
        vm.IsActive = updatedVm.IsActive;
        vm.HostName = updatedVm.HostName;
        vm.FQDN = updatedVm.FQDN;
        vm.IsHighlyAvailable = updatedVm.IsHighlyAvailable;
        vm.Template = updatedVm.Template;
        vm.BackupFrequency = updatedVm.BackupFrequency;
        vm.Availability = updatedVm.Availability;
        vm.Mode = updatedVm.Mode;
        vm.Host = updatedVm.Host;
        vm.Client = updatedVm.Client;
        vm.Poorten = updatedVm.Poorten;

        return Get(id);
    }

    private void SetDummyVirtualMachineList()
    {
        var vm1 = new VirtualMachineDto.Detail
        {
            Id = 1,
            Name = "VM-IT-1",
            HostName = "VM_JN58CE_2354",
            FQDN = "TBD",
            IsHighlyAvailable = true,
            StartDate = new DateTime(2022, 01, 15),
            EndDate = new DateTime(2023, 04, 14),
            Template = Template.TEMPLATES[ETemplate.ArtificialIntelligence],
            Availability = (EDay)((int)EDay.Monday + (int)EDay.Tuesday + (int)EDay.Wednesday + (int)EDay.Thursday + (int)EDay.Friday),
            BackupFrequency = EBackupFrequency.Daily,
            IsActive = true,
            CreatedAt = DateTime.Now,
            Poorten = "Poort 1, poort2",
            Host = "host123pt-45f",
            CPU = 4,
            RAM = 3,
            Storage = 950,
            Mode = EMode.IaaS
        };
        
        var vm2 = new VirtualMachineDto.Detail
        {
            Id = 2,
            Name = "VM-IT-2",
            HostName = "VM_GH35ZR_5436",
            FQDN = "TBD",
            IsHighlyAvailable = false,
            StartDate = new DateTime(2022, 02, 16),
            EndDate = new DateTime(2023, 03, 17),
            Template = Template.TEMPLATES[ETemplate.Database],
            Availability = (EDay)((int)EDay.Monday + (int)EDay.Tuesday + (int)EDay.Wednesday + (int)EDay.Thursday + (int)EDay.Friday + (int)EDay.Saturday + (int)EDay.Sunday),
            BackupFrequency = EBackupFrequency.Weekly,
            IsActive = false,
            CreatedAt = DateTime.Now,
            Poorten = "Poort 1, poort2",
            Host = "host123pt-45f",
            CPU = 2,
            RAM = 5,
            Storage = 920,
            Mode = EMode.IaaS
        };

        // TODO more vms

        _vms.Add(vm1);
        _vms.Add(vm2);
    }
}
