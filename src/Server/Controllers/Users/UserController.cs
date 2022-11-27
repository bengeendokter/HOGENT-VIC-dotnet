using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Shared.Clients;

namespace Server.Controllers.Users;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IClientService clientService;

    public UserController(IClientService clientService)
    {
        this.clientService = clientService;
    }
    
    [SwaggerOperation("Returns a list of clients and users.")]
    [HttpGet]
    public async Task<List<ClientDto.Index>> GetIndex()
    {
        return await clientService.GetIndexAsync();
    }
    /*
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
    }*/
}
