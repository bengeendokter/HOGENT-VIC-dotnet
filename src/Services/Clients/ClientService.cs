using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Shared.Clients;
using Domain.Users;
using EClientType = Domain.Users.EClientType;

namespace Services.Clients;

public class ClientService : IClientService
{
    private readonly VicDbContext dbContext;

    public ClientService(VicDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<List<ClientDto.Index>> GetIndexAsync(ClientRequest.Index request)
    {
        var query = dbContext.Clients.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Searchterm))
        {
            query = query.Where(x => x.Name.Contains(request.Searchterm, StringComparison.OrdinalIgnoreCase) ||
            x.Surname.Contains(request.Searchterm, StringComparison.OrdinalIgnoreCase) ||
            x.ClientOrganisation.Contains(request.Searchterm, StringComparison.OrdinalIgnoreCase) ||
            x.PhoneNumber.Contains(request.Searchterm, StringComparison.OrdinalIgnoreCase));
        }
        if (!string.IsNullOrWhiteSpace(request.ClientType))
        {
            var type = Enum.Parse<EClientType>(request.ClientType, true);
            query = query.Where(x => x.ClientType.Equals(type));
        }

        var items = await query
           .Skip((request.Page - 1) * request.PageSize)
           .Take(request.PageSize)
           .OrderBy(x => x.Id)
           .Select(x => new ClientDto.Index
           {
               Id = x.Id,
               Name = x.Name,
               Surname = x.Surname,
               PhoneNumber = x.PhoneNumber,
               ClientType = (Shared.Clients.EClientType) x.ClientType,
               ClientOrganisation = x.ClientOrganisation,
           })
           .ToListAsync();

        return items;
    }

    public async Task<ClientDto.Detail> GetDetailAsync(int clientId)
    {
        var client = await dbContext.Clients.Include(x => x.VirtualMachines).FirstOrDefaultAsync(x => x.Id == clientId);

        if (client is null)
            throw new EntityNotFoundException(nameof(Client), clientId);

        List<ClientVMDto.Index> lijst = new();

        foreach (var vm in client.VirtualMachines)
        {
            ClientVMDto.Index v = new()
            {
                Id = vm.Id,
                Name = vm.Name
            };
            lijst.Add(v);
        }

        return new ClientDto.Detail
        {
            Id = client.Id,
            Name = client.Name,
            Surname = client.Surname,
            PhoneNumber = client.PhoneNumber,
            ClientType = (Shared.Clients.EClientType)client.ClientType,
            ClientOrganisation = client.ClientOrganisation,
            Email = client.Email,
            BackupContact = client.BackupContact,
            Education = client.Education,
            ExternalType = client.ExternalType,
            VmList = lijst,
        };
    }

    public async Task<int> CreateAsync(ClientDto.Mutate model)
    {
        if (await dbContext.Clients.AnyAsync(x => x.Name == model.Name))
            throw new EntityAlreadyExistsException(nameof(Client), nameof(Client.Name), model.Name);

        Client client = new Client(
            model.Name!,
            model.Surname!,
            model.Email!,
            model.PhoneNumber!,
            model.BackupContact!,
            (Domain.Users.EClientType) model.ClientType,
            model.ClientOrganisation!,
            model.Education,
            model.ExternalType,
            true
        );

        dbContext.Clients.Add(client);
        await dbContext.SaveChangesAsync();
        
        return client.Id;
    }

    public async Task EditAsync(int clientId, ClientDto.Mutate model)
    {
        Client? client = await dbContext.Clients.SingleOrDefaultAsync(x => x.Id == clientId);

        if (client is null)
            throw new EntityNotFoundException(nameof(Client), clientId);

        client.Name = model.Name!;
        client.PhoneNumber = model.PhoneNumber!;
        client.Email = model.Email!;
        client.BackupContact = model.BackupContact!;
        client.ClientOrganisation = model.ClientOrganisation!;
        client.ClientType = (Domain.Users.EClientType) model.ClientType;
        client.Education = model.Education;
        client.ExternalType = model.ExternalType;

        await dbContext.SaveChangesAsync();
    }


    public async Task DeleteAsync(int clientId)
    {
        Client? client = await dbContext.Clients.SingleOrDefaultAsync(x => x.Id == clientId);

        if (client is null)
            throw new EntityNotFoundException(nameof(Client), clientId);

        dbContext.Clients.Remove(client);

        await dbContext.SaveChangesAsync();
    }
}
