namespace Shared.VirtualMachines;

public interface IVirtualMachineRequestService
{
    List<VirtualMachineRequest> GetAll();

    VirtualMachineRequest? Get(int id);

    //TODO: DTO
    VirtualMachineRequest? Update(int id, VirtualMachineRequest request);
}
