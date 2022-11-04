namespace Shared;

public class FakeUsageStatsService : IUsageService
{
    private List<UsageDTO.Index> _usages = new();

    public FakeUsageStatsService()
    {
        _usages.Add(new UsageDTO.Index() { UsageType = EUsage.Cpu, Unit = EUsageUnit.Cores, Total = 20, Used = 5 });
        _usages.Add(new UsageDTO.Index() { UsageType = EUsage.Ram, Unit = EUsageUnit.GB, Total= 64, Used = 48 });
        _usages.Add(new UsageDTO.Index() { UsageType = EUsage.Storage, Unit = EUsageUnit.GB, Total= 1000, Used = 594.6M });
    }

    public async Task<IEnumerable<UsageDTO.Index>> GetAllUsageStats()
    {
        await Task.Delay(1000);
        return _usages;
    }
}
