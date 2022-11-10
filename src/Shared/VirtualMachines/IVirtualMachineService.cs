namespace Shared.VirtualMachines;

public interface IVirtualMachineService
{
    List<VirtualMachineDto.Index> GetAll();

    VirtualMachine? Get(int id);

    // TODO: Dto
    VirtualMachine? Update(int id, VirtualMachine vm);
}