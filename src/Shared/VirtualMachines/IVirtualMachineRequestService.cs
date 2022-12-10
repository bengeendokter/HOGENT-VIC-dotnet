namespace Shared.VirtualMachines;

public interface IVirtualMachineRequestService
{
    Task<List<VirtualMachineRequestDto.Index>> GetAll(VirtualMachineRequestReq.Index request);

    Task<VirtualMachineRequestDto.Detail> Get(int id);

    Task<int> CreateAsync(VirtualMachineRequestDto.Create model);

    Task EditAsync(int id, VirtualMachineRequestDto.Detail request);

    Task<List<VirtualMachineRequestDto.Index>> GetRequestsFromClient(int clientId);
}
