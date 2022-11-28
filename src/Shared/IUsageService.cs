namespace Shared;

public interface IUsageService
{
    Task<List<UsageDto.Index>> GetAllUsageStats();
}
