﻿using System.Net.Http.Json;

namespace Client.VirtualMachines;

public class VirtualMachineService : IVirtualMachineService
{
    private readonly HttpClient client;
    private const string endpoint = "api/vm";

    public VirtualMachineService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<List<VirtualMachineDto.Index>> GetIndexAsync()
    {
        return await client.GetFromJsonAsync<List<VirtualMachineDto.Index>>(endpoint) ?? new();
    }

    public async Task<VirtualMachineDto.Detail> GetDetailAsync(int virtualMachineId)
    {
        var response = await client.GetFromJsonAsync<VirtualMachineDto.Detail>(
            $"{endpoint}/{virtualMachineId}"
        );
        return response!;
    }

    public async Task<int> CreateAsync(VirtualMachineDto.Mutate model)
    {
        var response = await client.PostAsJsonAsync(endpoint, model);
        return await response.Content.ReadFromJsonAsync<int>();
    }

    public async Task EditAsync(int virtualMachineId, VirtualMachineDto.Mutate model)
    {
        await client.PutAsJsonAsync($"{endpoint}/{virtualMachineId}", model);
    }
}
