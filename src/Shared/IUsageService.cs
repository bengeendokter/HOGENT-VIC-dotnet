namespace Shared;

public interface IUsageService
{
    Task<IEnumerable<UsageDTO.Index>> getAllUsageStats();
}
