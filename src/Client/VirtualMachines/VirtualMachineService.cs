namespace Client.VirtualMachines;

public class VirtualMachineService : IVirtualMachineService
{
    public VirtualMachineService() { }

    public async Task<List<VirtualMachineDto.Index>> GetIndexAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<VirtualMachineDto.Detail> GetDetailAsync(int virtualMachineId)
    {
        throw new NotImplementedException();
    }

    public async Task<int> CreateAsync(VirtualMachineDto.Mutate model)
    {
        throw new NotImplementedException();
    }

    public async Task EditAsync(int virtualMachineId, VirtualMachineDto.Mutate model)
    {
        throw new NotImplementedException();
    }
}
