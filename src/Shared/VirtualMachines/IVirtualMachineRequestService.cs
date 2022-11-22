namespace Shared.VirtualMachines;

public interface IVirtualMachineRequestService
{
    List<VirtualMachineRequestDto.Detail> GetAll();

    VirtualMachineRequestDto.Detail? Get(int id);

    VirtualMachineRequestDto.Detail? Update(int id, VirtualMachineRequestDto.Detail request);
}
