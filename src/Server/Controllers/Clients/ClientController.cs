using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Clients;
using Shared.VirtualMachines;
using Swashbuckle.AspNetCore.Annotations;

namespace Server.Controllers.Clients;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ClientController : ControllerBase
{
    private readonly IClientService clientService;
    private readonly IVirtualMachineService virtualMachineService;

    public ClientController(IClientService clientService, IVirtualMachineService virtualMachineService)
    {
        this.clientService = clientService;
        this.virtualMachineService = virtualMachineService;
    }

    [Authorize(Roles = "Administrator, Moderator")]
    [SwaggerOperation("Returns a list of clients.")]
    [HttpGet]
    public async Task<List<ClientDto.Index>> GetIndex([FromQuery] ClientRequest.Index request)
    {
        return await clientService.GetIndexAsync(request);
    }

    [Authorize(Roles = "Administrator, Moderator, Customer")]
    [SwaggerOperation("Returns a specific client by id.")]
    [HttpGet("{clientId}")]
    public async Task<ClientDto.Detail> GetDetail(int clientId)
    {
        return await clientService.GetDetailAsync(clientId);
    }

    [Authorize(Roles = "Administrator, Moderator")]
    [SwaggerOperation("Creates a new client.")]
    [HttpPost]
    public async Task<IActionResult> Create(ClientDto.Mutate model)
    {
        var clientId = await clientService.CreateAsync(model);
        return CreatedAtAction(nameof(Create), clientId);
    }

    [Authorize(Roles = "Administrator, Moderator")]
    [SwaggerOperation("Edites an existing client.")]
    [HttpPut("{clientId}")]
    public async Task<IActionResult> Edit(int clientId, ClientDto.Mutate model)
    {
        await clientService.EditAsync(clientId, model);
        return NoContent();
    }

    [Authorize(Roles = "Administrator, Moderator")]
    [SwaggerOperation("Deletes an existing client.")]
    [HttpDelete("{clientId}")]
    public async Task<IActionResult> Delete(int clientId)
    {
        await clientService.DeleteAsync(clientId);
        return NoContent();
    }
}
