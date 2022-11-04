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
            StartDate = new DateTime(2022, 01, 15),
            EndDate = new DateTime(2023, 02, 14),
            Template = Template.TEMPLATES[ETemplate.ArtificialIntelligence],
            Availability = (EDay)((int)EDay.Monday + (int)EDay.Tuesday + (int)EDay.Wednesday + (int)EDay.Thursday + (int)EDay.Friday),
            EBackupFrequency = EBackupFrequency.Daily,
            IsActive = true,
            CreateDate = DateTime.Now,
            Poorten = "Poort 1, poort2",
            Host = "host123pt-45f",
            CPU = 2,
            RAM = 5,
            Storage = 920,
            Mode = EMode.IaaS
        };
        var vm2 = new VirtualMachine
        {
            Id = 2,
            Name = "VM-IT-2",
            HostName = "VM_ADJC_4562",
            FQDN = "TBD",
            IsHighAvailable = false,
            StartDate = new DateTime(2022, 03, 02),
            EndDate = new DateTime(2023, 04, 10),
            Template = Template.TEMPLATES[ETemplate.Database],
            Availability = (EDay)((int)EDay.Monday + (int)EDay.Tuesday + (int)EDay.Wednesday + (int)EDay.Thursday + (int)EDay.Friday),
            EBackupFrequency = EBackupFrequency.Weekly,
            IsActive = true,
            CreateDate = DateTime.Now,
            Poorten = "Poort 1",
            Host = "host123pt-45f",
            CPU = 4,
            RAM = 16,
            Storage = 512,
            Mode = EMode.SaaS
        };
        var rand = new Random();

        for (int i=1; i <=  10; i++)
        {
            var act = new Activity
            {
                VirtualMachine = (rand.Next(2) == 0 ? vm : vm2),
                Date = new DateTime(2022, 01, i),
                Type = (rand.Next(2) == 0 ? EActivity.Added : EActivity.Deleted)
            };

            _activities.Add(act);
        }
    }

    public List<Activity> GetAll()
    {
        /* tijdelijk sorteren op datum, later sortering verplaatsen naar backend */
        return _activities.OrderByDescending(a => a.Date).ToList();
    }
}
