namespace Shared.VirtualMachines;

public interface IVirtualMachineService
{
    Task<List<VirtualMachineDto.Index>> GetIndexAsync(VirtualMachineRequest.Index request);
    Task<VirtualMachineDto.Detail> GetDetailAsync(int virtualMachineId);
    Task<int> CreateAsync(VirtualMachineDto.Mutate model);
    Task EditAsync(int virtualMachineId, VirtualMachineDto.Mutate model);
    Task DeleteAsync(int virtualMachineId);
}
