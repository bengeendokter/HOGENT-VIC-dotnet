using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Shared.VirtualMachines;

namespace Server.Controllers.VirtualMachines;

[ApiController]
[Route("api/[controller]")]
public class VirtualMachineRequestController : ControllerBase
{
    private readonly IVirtualMachineRequestService virtualMachineRequestService;

    public VirtualMachineRequestController(IVirtualMachineRequestService virtualMachineRequestService)
    {
        this.virtualMachineRequestService = virtualMachineRequestService;
    }

    [SwaggerOperation("Returns a list of requests.")]
    [HttpGet]
    public async Task<List<VirtualMachineRequestDto.Detail>> GetAll()
    {
        return await virtualMachineRequestService.GetAll();
    }

    [SwaggerOperation("Returns a specific request by id.")]
    [HttpGet("{id}")]
    public async Task<VirtualMachineRequestDto.Detail> Get(int id)
    {
        return await virtualMachineRequestService.Get(id);
    }

    [SwaggerOperation("Creates a new request.")]
    [HttpPost]
    public async Task<IActionResult> Create(VirtualMachineRequestDto.Create model)
    {
        var id = await virtualMachineRequestService.CreateAsync(model);
        return CreatedAtAction(nameof(Create), id);
    }

    [SwaggerOperation("Edites an existing request.")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Edit(int id, VirtualMachineRequestDto.Detail model)
    {
        await virtualMachineRequestService.EditAsync(id, model);
        return NoContent();
    }
}
