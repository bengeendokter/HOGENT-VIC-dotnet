namespace Shared;

public class ActivityService : IActivityService
{
    private readonly List<Activity> _activities = new List<Activity>();

    public ActivityService()
    {
        SetDummyActivities();
    }

    private void SetDummyActivities()
    {
        _activities.Clear();
        var vm = new VirtualMachine
        {
            Id = 1,
            Name = "VM-IT-1",
            HostName = "VM_JN58CE_2354",
            FQDN = "TBD",
            IsHighAvailable = true,
            StartDate = new DateTime(2022, 01, 01),
            EndDate = new DateTime(2022, 04, 02),
            Template = Template.TEMPLATES[ETemplate.ArtificialIntelligence],
            Availability = (EDay)((int)EDay.Monday + (int)EDay.Tuesday + (int)EDay.Wednesday + (int)EDay.Thursday + (int)EDay.Friday),
            EBackupFrequency = EBackupFrequency.Daily,
            IsActive = true,
            CreateDate = new DateTime(2022, 01, 01),
            Poorten = "Poort 1, poort2",
            Host = "host123pt-45f",
            CPU = 6,
            RAM = 32,
            Storage = 25,
            Mode = EMode.IaaS
        };
        var vm2 = new VirtualMachine
        {
            Id = 2,
            Name = "VM-IT-2",
            HostName = "VM_ADJC_4562",
            FQDN = "TBD",
            IsHighAvailable = false,
            StartDate = new DateTime(2022, 04, 01),
            EndDate = new DateTime(2022, 06, 01),
            Template = Template.TEMPLATES[ETemplate.Database],
            Availability = (EDay)((int)EDay.Monday + (int)EDay.Tuesday + (int)EDay.Wednesday + (int)EDay.Thursday + (int)EDay.Friday),
            EBackupFrequency = EBackupFrequency.Weekly,
            IsActive = true,
            CreateDate = new DateTime(2022, 04, 01),
            Poorten = "Poort 1",
            Host = "host123pt-45f",
            CPU = 2,
            RAM = 16,
            Storage = 60,
            Mode = EMode.SaaS
        };

        var vm3 = new VirtualMachine
        {
            Id = 2,
            Name = "VM-IT-3",
            HostName = "VM_ADJC_4562",
            FQDN = "TBD",
            IsHighAvailable = false,
            StartDate = new DateTime(2022, 05, 01),
            EndDate = new DateTime(2022, 07, 01),
            Template = Template.TEMPLATES[ETemplate.MachineLearning],
            Availability = (EDay)((int)EDay.Monday + (int)EDay.Tuesday + (int)EDay.Wednesday + (int)EDay.Thursday + (int)EDay.Friday),
            EBackupFrequency = EBackupFrequency.Weekly,
            IsActive = true,
            CreateDate = new DateTime(2022, 05, 01),
            Poorten = "Poort 1",
            Host = "host123pt-45f",
            CPU = 8,
            RAM = 64,
            Storage = 80,
            Mode = EMode.SaaS
        };

        //vm1 toegevoegd
        var act1 = new Activity
        {
            VirtualMachine = (vm),
            Date = vm.StartDate,
            Type = EActivity.Added
        };

        //vm2 toegevoegd
        var act2 = new Activity
        {
            VirtualMachine = (vm2),
            Date = vm2.StartDate,
            Type = EActivity.Added
        };

        //vm1 verwijderd
        var act3 = new Activity
        {
            VirtualMachine = (vm),
            Date = vm.EndDate,
            Type = EActivity.Deleted
        };

        //vm3 toegevoegd
        var act4 = new Activity
        {
            VirtualMachine = (vm3),
            Date = vm3.StartDate,
            Type = EActivity.Added
        };

        //vm2 verwijderd
        var act5 = new Activity
        {
            VirtualMachine = (vm2),
            Date = vm2.EndDate,
            Type = EActivity.Deleted
        };

        //vm3 verwijderd
        var act6 = new Activity
        {
            VirtualMachine = (vm3),
            Date = vm3.EndDate,
            Type = EActivity.Deleted
        };

        _activities.Add(act1);
        _activities.Add(act2);
        _activities.Add(act3);
        _activities.Add(act4);
        _activities.Add(act5);
        _activities.Add(act6);
    }

    public List<Activity> GetAll()
    {
        /* tijdelijk sorteren op datum, later sortering verplaatsen naar backend */
        return _activities.OrderByDescending(a => a.Date).ToList();
    }
}
