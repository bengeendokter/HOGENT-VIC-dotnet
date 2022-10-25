namespace Shared
{
    public interface IVirtualMachineService
    {
        List<VirtualMachine> GetAll();

        VirtualMachine? Get(int id);

        // TODO: Dto
        VirtualMachine? Update(int id, VirtualMachine vm);
    }
}