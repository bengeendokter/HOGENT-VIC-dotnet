namespace Shared.VirtualMachines;

public interface IVirtualMachineService
{
    Task<List<VirtualMachineDto.Index>> GetIndexAsync();
    Task<VirtualMachineDto.Detail> GetDetailAsync(int virtualMachineId);
    Task MutateAsync(int virtualMachineId, VirtualMachineDto.Mutate model);
}