namespace Client.VirtualMachines;

public class VirtualMachineService : IVirtualMachineService
{
    private readonly List<VirtualMachine> _vms = new();

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
            StartDate = vm.StartDate,
            EndDate = vm.EndDate,
            IsActive = vm.IsActive,
            HostName = vm.HostName,
            FQDN = vm.FQDN,
            Availability = vm.Availability,
            BackupFrequency = vm.BackupFrequency,
            IsHighlyAvailable = vm.IsHighlyAvailable,
            Template = vm.Template
        };
    }

    public List<VirtualMachineDto.Index> GetAll()
    {
        return _vms.Select(v => new VirtualMachineDto.Index
        {
            Id = v.Id,
            Name = v.Name,
            StartDate = v.StartDate,
            EndDate = v.EndDate,
            IsActive = v.IsActive
        }).ToList();
    }

    public VirtualMachineDto.Detail? Update(int id, VirtualMachineDto.Mutate updatedVm)
    {
        var vm = _vms.FirstOrDefault(v => v.Id == id);
        if (vm == null) return null;

        vm.StartDate = updatedVm.StartDate;
        vm.EndDate = updatedVm.EndDate;
        vm.IsActive = updatedVm.IsActive;
        vm.HostName = updatedVm.HostName;
        vm.FQDN = updatedVm.FQDN;
        vm.IsHighlyAvailable = updatedVm.IsHighlyAvailable;
        vm.Template = updatedVm.Template;
        vm.BackupFrequency = updatedVm.BackupFrequency;
        vm.Availability = updatedVm.Availability;
        vm.Client = updatedVm.Client;

        return Get(id);
    }

    private void SetDummyVirtualMachineList()
    {
        var vm1 = new VirtualMachine
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
            CreateDate = DateTime.Now,
        };
        
        var vm2 = new VirtualMachine
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
            CreateDate = DateTime.Now,
        };

        // TODO more vms

        _vms.Add(vm1);
        _vms.Add(vm2);
    }
}
