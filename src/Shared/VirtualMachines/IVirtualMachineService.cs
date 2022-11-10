namespace Shared.VirtualMachines;

public interface IVirtualMachineService
{
    List<VirtualMachineDto.Index> GetAll();

    VirtualMachineDto.Detail? Get(int id);

    // TODO: Dto
    VirtualMachine? Update(int id, VirtualMachine vm);
}