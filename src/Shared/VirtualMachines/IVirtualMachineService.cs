﻿namespace Shared.VirtualMachines;

public interface IVirtualMachineService
{
    Task<List<VirtualMachineDto.Index>> GetIndexAsync();
    Task<VirtualMachineDto.Detail> GetDetailAsync(int virtualMachineId);
    Task<int> CreateAsync(VirtualMachineDto.Mutate model);
    Task EditAsync(int virtualMachineId, VirtualMachineDto.Mutate? model, int? clientId = -1);
}
