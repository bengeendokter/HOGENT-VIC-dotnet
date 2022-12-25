using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.VirtualMachines;
using Swashbuckle.AspNetCore.Annotations;

namespace Server.Controllers.VirtualMachines;

[ApiController]
[Route("api/[controller]")]
// [Authorize]
public class VirtualMachineRequestController : ControllerBase
{
    private readonly IVirtualMachineRequestService virtualMachineRequestService;

    public VirtualMachineRequestController(IVirtualMachineRequestService virtualMachineRequestService)
    {
        this.virtualMachineRequestService = virtualMachineRequestService;
    }

    // [Authorize(Roles = "Administrator, Moderator")]
    [AllowAnonymous]
    [SwaggerOperation("Returns a list of requests.")]
    [HttpGet]
    public async Task<List<VirtualMachineRequestDto.Index>> GetAll([FromQuery] VirtualMachineRequestReq.Index request)
    {
        return await virtualMachineRequestService.GetAll(request);
    }

    // [Authorize(Roles = "Administrator, Moderator")]
    [AllowAnonymous]
    [SwaggerOperation("Returns a specific request by id.")]
    [HttpGet("{id}")]
    public async Task<VirtualMachineRequestDto.Detail> Get(int id)
    {
        return await virtualMachineRequestService.Get(id);
    }

    [Authorize(Roles = "Administrator, Moderator")]
    [SwaggerOperation("Returns requests from a client.")]
    [HttpGet("client/{id}")]
    public async Task<List<VirtualMachineRequestDto.Index>> GetRequestsFromClient(int id)
    {
        return await virtualMachineRequestService.GetRequestsFromClient(id);
    }

    [Authorize(Roles = "Customer, Administrator, Moderator")]
    [SwaggerOperation("Creates a new request.")]
    [HttpPost]
    public async Task<IActionResult> Create(VirtualMachineRequestDto.Create model)
    {
        var id = await virtualMachineRequestService.CreateAsync(model);
        return CreatedAtAction(nameof(Create), id);
    }

    [Authorize(Roles = "Administrator, Moderator")]
    [SwaggerOperation("Edites an existing request.")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Edit(int id, VirtualMachineRequestDto.Detail model)
    {
        await virtualMachineRequestService.EditAsync(id, model);
        return NoContent();
    }
}
