namespace Shared
{
    public interface IVirtualMachineService
    {
        List<VirtualMachine> GetAll();
        // TODO: Dto
        VirtualMachine? Update(int id, VirtualMachine vm);
    }
}