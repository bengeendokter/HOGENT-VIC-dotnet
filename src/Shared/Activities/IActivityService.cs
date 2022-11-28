using Shared.VirtualMachines;

namespace Shared.Activities;

public interface IActivityService
{
    Task<List<ActivityDto.Index>> GetIndexAsync(ActivityRequest.Index request);
}
