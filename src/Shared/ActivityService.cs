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
        var rand = new Random();

        for (int i=1; i <=  10; i++)
        {
            var act = new Activity
            {
                VirtualMachine = vm,
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
