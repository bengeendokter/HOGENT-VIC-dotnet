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
            query = query.Where(x => x.Name.Contains(request.Searchterm, StringComparison.OrdinalIgnoreCase));
        }
        if (request.ClientType is not null)
        {
            query = query.Where(x => x.ClientType.Equals(request.ClientType.Value));
        }

        var items = await query
//         .Skip((request.Page - 1) * request.PageSize)
//         .Take(request.PageSize)
           .OrderBy(x => x.Id)
           .Select(x => new ClientDto.Index
           {
               Id = x.Id,
               Name = x.Name,
               PhoneNumber = x.PhoneNumber,
               ClientType = (Shared.Clients.EClientType) x.ClientType,
               ClientOrganisation = x.ClientOrganisation,
           })
           .ToListAsync();

        return items;
    }

    public async Task<ClientDto.Detail> GetDetailAsync(int clientId)
    {
        var client = await dbContext.Clients.FirstOrDefaultAsync(x => x.Id == clientId);

        if (client is null)
            throw new EntityNotFoundException(nameof(Client), clientId);

        return new ClientDto.Detail
        {
            Id = client.Id,
            Name = client.Name,
            PhoneNumber = client.PhoneNumber,
            ClientType = (Shared.Clients.EClientType) client.ClientType,
            ClientOrganisation = client.ClientOrganisation,
            Email = client.Email,
            BackupContact = client.BackupContact,
            Education = client.Education,
            ExternalType = client.ExternalType,
        };
    }

    public async Task<int> CreateAsync(ClientDto.Mutate model)
    {
        if (await dbContext.Clients.AnyAsync(x => x.Name == model.Name))
            throw new EntityAlreadyExistsException(nameof(Client), nameof(Client.Name), model.Name);

        Client client = new Client(
            model.Name!,
            model.Email!,
            model.PhoneNumber!,
            model.BackupContact!,
            (Domain.Users.EClientType) model.ClientType,
            model.ClientOrganisation!,
            model.Education,
            model.ExternalType
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
