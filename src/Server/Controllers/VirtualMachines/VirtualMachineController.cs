using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Shared.VirtualMachines;
using Shared.Clients;
using Microsoft.AspNetCore.Authorization;

namespace Server.Controllers.VirtualMachines;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class VirtualMachineController : ControllerBase
{
    private readonly IVirtualMachineService virtualMachineService;

    public VirtualMachineController(IVirtualMachineService virtualMachineService)
    {
        this.virtualMachineService = virtualMachineService;
    }

    [Authorize(Roles = "Administrator, Moderator")]
    [SwaggerOperation("Returns a list of virtual machines.")]
    [HttpGet]
    public async Task<List<VirtualMachineDto.Index>> GetIndex([FromQuery] VirtualMachineReq.Index request)
    {
        return await virtualMachineService.GetIndexAsync(request);
    }

    [Authorize(Roles = "Administrator, Moderator")]
    [SwaggerOperation("Returns a specific virtual machine by id.")]
    [HttpGet("{virtualMachineId}")]
    public async Task<VirtualMachineDto.Detail> GetDetail(int virtualMachineId)
    {
        return await virtualMachineService.GetDetailAsync(virtualMachineId);
    }

    [Authorize(Roles = "Administrator, Moderator")]
    [SwaggerOperation("Creates a new virtual machine.")]
    [HttpPost]
    public async Task<IActionResult> Create(VirtualMachineDto.Mutate model)
    {
        var virtualMachineId = await virtualMachineService.CreateAsync(model);
        return CreatedAtAction(nameof(Create), virtualMachineId);
    }

    [Authorize(Roles = "Administrator, Moderator")]
    [SwaggerOperation("Edites an existing virtual machine.")]
    [HttpPut("{virtualMachineId}")]
    public async Task<IActionResult> Edit(int virtualMachineId, VirtualMachineDto.Mutate model)
    {
        await virtualMachineService.EditAsync(virtualMachineId, model);
        return NoContent();
    }

    [Authorize(Roles = "Administrator, Moderator")]
    [SwaggerOperation("Deletes an existing virtual machine.")]
    [HttpDelete("{virtualMachineId}")]
    public async Task<IActionResult> Delete(int virtualMachineId)
    {
        await virtualMachineService.DeleteAsync(virtualMachineId);
        return NoContent();
    }
}
