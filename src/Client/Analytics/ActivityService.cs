namespace Client.Analytics;

public class ActivityService : IActivityService
{
    private readonly List<Activity> _activities = new List<Activity>();

    public ActivityService()
    {
        SetDummyActivities();
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

        } else
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

    private void SetDummyActivities()
    {
        _activities.Clear();
        var vm = new VirtualMachineDto.Index
        {
            Id = 1,
            Name = "VM-IT-1",
            StartDate = DatumCreator(0, 0),
            EndDate = DatumCreator(3, 1),
            IsActive = true,
            CPU = 6,
            RAM = 32,
            Storage = 25,
        };
        var vm2 = new VirtualMachineDto.Index
        {
            Id = 2,
            Name = "VM-IT-2",
            StartDate = DatumCreator(3, 0),
            EndDate = DatumCreator(5, 0),
            IsActive = true,
            CPU = 2,
            RAM = 16,
            Storage = 60,
        };

        var vm3 = new VirtualMachineDto.Index
        {
            Id = 2,
            Name = "VM-IT-3",
            StartDate = DatumCreator(4, 0),
            EndDate = DatumCreator(6, 0),
            IsActive = true,
            CPU = 8,
            RAM = 64,
            Storage = 80,
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