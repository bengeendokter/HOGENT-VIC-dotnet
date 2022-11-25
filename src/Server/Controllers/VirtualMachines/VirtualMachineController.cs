using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Shared.VirtualMachines;

namespace Server.Controllers.VirtualMachines;

[ApiController]
[Route("api/[controller]")]
public class VirtualMachineController : ControllerBase
{
    private readonly IVirtualMachineService virtualMachineService;

    public VirtualMachineController(IVirtualMachineService virtualMachineService)
    {
        this.virtualMachineService = virtualMachineService;
    }

    [SwaggerOperation("Returns a list of virtual machines.")]
    [HttpGet]
    public async Task<List<VirtualMachineDto.Index>> GetIndex()
    {
        return await virtualMachineService.GetIndexAsync();
    }

    [SwaggerOperation("Returns a specific virtual machine by id.")]
    [HttpGet("{virtualMachineId}")]
    public async Task<VirtualMachineDto.Detail> GetDetail(int virtualMachineId)
    {
        return await virtualMachineService.GetDetailAsync(virtualMachineId);
    }

    [SwaggerOperation("Creates a new virtual machine.")]
    [HttpPost]
    public async Task<IActionResult> Create(VirtualMachineDto.Mutate model)
    {
        var virtualMachineId = await virtualMachineService.CreateAsync(model);
        return CreatedAtAction(nameof(Create), virtualMachineId);
    }

    [SwaggerOperation("Edites an existing virtual machine.")]
    [HttpPut("{virtualMachineId}")]
    public async Task<IActionResult> Edit(int virtualMachineId, VirtualMachineDto.Mutate model)
    {
        await virtualMachineService.EditAsync(virtualMachineId, model);
        return NoContent();
    }
}