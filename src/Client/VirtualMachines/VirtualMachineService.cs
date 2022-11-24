namespace Client.VirtualMachines;

public class VirtualMachineService : IVirtualMachineService
{
    private readonly List<VirtualMachineDto.Detail> _vms = new();

    public VirtualMachineService()
    {
        SetDummyVirtualMachineList();
    }

    public Task<VirtualMachineDto.Detail> GetDetailAsync(int virtualMachineId)
    {
        var vm = _vms.First(v => v.Id == virtualMachineId);

        return Task.FromResult(new VirtualMachineDto.Detail
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
        });
    }

    public Task<List<VirtualMachineDto.Index>> GetIndexAsync()
    {
        return Task.FromResult(_vms.Select(v => new VirtualMachineDto.Index
        {
            Id = v.Id,
            Name = v.Name,
            CPU = v.CPU,
            RAM = v.RAM,
            Storage = v.Storage,
            StartDate = v.StartDate,
            EndDate = v.EndDate,
            IsActive = v.IsActive
        }).ToList());
    }

    public Task MutateAsync(int virtualMachineId, VirtualMachineDto.Mutate model)
    {
        var vm = _vms.FirstOrDefault(v => v.Id == virtualMachineId);
        if (vm == null) return Task.CompletedTask;

        vm.Name = model.Name;
        vm.CPU = model.CPU;
        vm.RAM = model.RAM;
        vm.Storage = model.Storage;
        vm.StartDate = model.StartDate;
        vm.EndDate = model.EndDate;
        vm.IsActive = model.IsActive;
        vm.HostName = model.HostName;
        vm.FQDN = model.FQDN;
        vm.IsHighlyAvailable = model.IsHighlyAvailable;
        vm.Template = model.Template;
        vm.BackupFrequency = model.BackupFrequency;
        vm.Availability = model.Availability;
        vm.Mode = model.Mode;
        vm.Host = model.Host;
        vm.Client = model.Client;
        vm.Poorten = model.Poorten;

        return GetDetailAsync(virtualMachineId);
    }

    private DateTime DatumCreator(int maand, int dag)
    {
        int j = DateTime.Now.Year;
        int m = DateTime.Now.Month;
        int d = DateTime.Now.Day;

        var dagen = DateTime.DaysInMonth(j, m);

        int j1 = j;
        int m1 = m;
        int d1 = d;

        if (d1 + dag > dagen)
        {
            d1 = (d1 + dag) % dagen;
            m1 += 1;

        }
        else
        {
            d1 += dag;
        }

        if (m1 + maand > 12)
        {
            m1 = (m1 + maand) % 12;
            j1 += 1;

        }
        else
        {
            m1 += maand;
        }

        return new DateTime(j1, m1, d1);
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
            StartDate = DatumCreator(0, 0),
            EndDate = DatumCreator(3, 1),
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
            StartDate = DatumCreator(3, 0),
            EndDate = DatumCreator(5, 0),
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

        var vm3 = new VirtualMachineDto.Detail
        {
            Id = 3,
            Name = "VM-IT-3",
            HostName = "VM_JN58CE_2354",
            FQDN = "TBD",
            IsHighlyAvailable = true,
            StartDate = DatumCreator(4, 0),
            EndDate = DatumCreator(6, 0),
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

        var vm4 = new VirtualMachineDto.Detail
        {
            Id = 4,
            Name = "VM-IT-4",
            HostName = "VM_GH35ZR_5436",
            FQDN = "TBD",
            IsHighlyAvailable = false,
            StartDate = DatumCreator(0, 5),
            EndDate = DatumCreator(3, 6),
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

        var vm5 = new VirtualMachineDto.Detail
        {
            Id = 5,
            Name = "VM-IT-5",
            HostName = "VM_JN58CE_2354",
            FQDN = "TBD",
            IsHighlyAvailable = true,
            StartDate = DatumCreator(3, 4),
            EndDate = DatumCreator(5, 4),
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

        var vm6 = new VirtualMachineDto.Detail
        {
            Id = 6,
            Name = "VM-IT-6",
            HostName = "VM_GH35ZR_5436",
            FQDN = "TBD",
            IsHighlyAvailable = false,
            StartDate = DatumCreator(4, 7),
            EndDate = DatumCreator(6, 7),
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

        _vms.Add(vm1);
        _vms.Add(vm2);
        _vms.Add(vm3);
        _vms.Add(vm4);
        _vms.Add(vm5);
        _vms.Add(vm6);
    }
}
