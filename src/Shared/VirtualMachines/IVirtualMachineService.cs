namespace Shared.VirtualMachines;

public interface IVirtualMachineService
{
    List<VirtualMachineDto.Index> GetAll();

    VirtualMachineDto.Detail? Get(int id);

    VirtualMachineDto.Detail? Update(int id, VirtualMachineDto.Mutate vm);
}