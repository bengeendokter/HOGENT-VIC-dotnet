namespace Shared.VirtualMachines;

public interface IVirtualMachineRequestService
{
    Task<List<VirtualMachineRequestDto.Detail>> GetAll();

    Task<VirtualMachineRequestDto.Detail> Get(int id);

    Task<int> CreateAsync(VirtualMachineRequestDto.Create model);

    Task EditAsync(int id, VirtualMachineRequestDto.Detail request);
}
