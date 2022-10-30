using System;

namespace Shared;

public class FakeUsageStatsService : IUsageService
{
    private List<UsageDTO.Index> _usages = new();

    public FakeUsageStatsService()
    {
        _usages.Add(new UsageDTO.Index() { UsageType=EUsageType.Cpu, Eenheid=EUsageTypeUnit.Cores, Total=20, Gebruikt=5});
        _usages.Add(new UsageDTO.Index() { UsageType = EUsageType.Ram, Eenheid = EUsageTypeUnit.GB, Total= 64, Gebruikt = 48 });
        _usages.Add(new UsageDTO.Index() { UsageType = EUsageType.Storage, Eenheid = EUsageTypeUnit.GB, Total= 1000, Gebruikt = 594.6M });
    }

    public async Task<IEnumerable<UsageDTO.Index>> getAllUsageStats()
    {
        await Task.Delay(1000);
        return _usages;
    }
}
