using Microsoft.AspNetCore.Mvc;
using Shared.Clients;
using Shared.VirtualMachines;
using Swashbuckle.AspNetCore.Annotations;

namespace Server.Controllers.Clients;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientService clientService;
    private readonly IVirtualMachineService virtualMachineService;

    public ClientController(IClientService clientService, IVirtualMachineService virtualMachineService)
    {
        this.clientService = clientService;
        this.virtualMachineService = virtualMachineService;
    }

    [SwaggerOperation("Returns a list of clients.")]
    [HttpGet]
    public async Task<List<ClientDto.Index>> GetIndex([FromQuery] ClientRequest.Index request)
    {
        return await clientService.GetIndexAsync(request);
    }

    [SwaggerOperation("Returns a specific client by id.")]
    [HttpGet("{clientId}")]
    public async Task<ClientDto.Detail> GetDetail(int clientId)
    {
        return await clientService.GetDetailAsync(clientId);
    }

    [SwaggerOperation("Creates a new client.")]
    [HttpPost]
    public async Task<IActionResult> Create(ClientDto.Mutate model)
    {
        var clientId = await clientService.CreateAsync(model);
        return CreatedAtAction(nameof(Create), clientId);
    }

    [SwaggerOperation("Edites an existing client.")]
    [HttpPut("{clientId}")]
    public async Task<IActionResult> Edit(int clientId, ClientDto.Mutate model)
    {
        await clientService.EditAsync(clientId, model);
        return NoContent();
    }

    [SwaggerOperation("Deletes an existing client.")]
    [HttpDelete("{clientId}")]
    public async Task<IActionResult> Delete(int clientId)
    {
        await clientService.DeleteAsync(clientId);
        return NoContent();
    }
}
