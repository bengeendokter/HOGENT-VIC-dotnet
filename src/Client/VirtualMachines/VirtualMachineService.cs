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

        //maand check
        var specifiek = DateTime.DaysInMonth(j1, m1);
        if (d1 > specifiek)
        {
            m1 += 1;
            d1 = d1 % specifiek;
        }

        //jaar check
        if (m1 > 12)
        {
            m1 = m1 % 12;
            j1 += 1;
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
