using Microsoft.EntityFrameworkCore;
using Persistence;
using Domain.Activities;
using Shared.Activities;
using Shared.VirtualMachines;
using Shared.Clients;

namespace Services.Activities;
public class ActivityService : IActivityService
{
    private readonly VicDbContext dbContext;

    public ActivityService(VicDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<List<ActivityDto.Index>> GetIndexAsync(ActivityRequest.Index request)
    {
        var query = dbContext.Activities.AsQueryable();


        if (!string.IsNullOrEmpty(request.Status))
        {
            var status = Enum.Parse<Domain.Activities.EActivity>(request.Status, true);
            query = query.Where(x => x.Type.Equals(status));
        }

        if (!string.IsNullOrWhiteSpace(request.SortBy))
        {
            query = SortActivityQuery(request.SortBy, query);
        }

        var items = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(a => new ActivityDto.Index
            {
                Id = a.Id,
                CreatedAt = a.CreatedAt,
                VirtualMachine = new VirtualMachineDto.Index
                {
                    Id = a.VirtualMachine.Id,
                    Name = a.VirtualMachine.Name,
                    CPU = a.VirtualMachine.CPU,
                    RAM = a.VirtualMachine.RAM,
                    Storage = a.VirtualMachine.Storage,
                    StartDate = a.VirtualMachine.StartDate,
                    EndDate = a.VirtualMachine.EndDate,
                    IsActive = a.VirtualMachine.IsActive,
                    Template = (ETemplate) a.VirtualMachine.Template,
                    IsHighlyAvailable = a.VirtualMachine.IsHighlyAvailable,
                    BackupFrequency = (EBackupFrequency) a.VirtualMachine.BackupFrequency,
                    Client = new ClientDto.Index
                    {
                        Name = a.VirtualMachine.Client.Name,
                        Surname = a.VirtualMachine.Client.Surname
                    }

                },
                Type = (Shared.Activities.EActivity) a.Type
            })
            .ToListAsync();

        return items;
    }

    private IQueryable<Activity> SortActivityQuery(string? sortby, IQueryable<Activity> query)
    {

        var sortingList = new List<Domain.Activities.EActivity> 
        { 
            Domain.Activities.EActivity.Added, 
            Domain.Activities.EActivity.Deleted
        };


        return (sortby) switch
        {
            "date" => query.OrderBy(x => x.CreatedAt),
            "name" => query.OrderBy(x => x.VirtualMachine.Name),
            "nameDesc" => query.OrderByDescending(x => x.VirtualMachine.Name),
            "cpu" => query
                    .OrderBy(x => sortingList.IndexOf(x.Type))
                    .ThenByDescending(x => x.VirtualMachine!.CPU),
            "cpuDesc" => query
                    .OrderByDescending(x => sortingList.IndexOf(x.Type))
                    .ThenByDescending(x => x.VirtualMachine!.CPU),
            "ram" => query
                    .OrderBy(x => sortingList.IndexOf(x.Type))
                    .ThenByDescending(x => x.VirtualMachine!.RAM),
            "ramDesc" => query
                    .OrderByDescending(x => sortingList.IndexOf(x.Type))
                    .ThenByDescending(x => x.VirtualMachine!.RAM),
            "storage" => query
                    .OrderBy(x => sortingList.IndexOf(x.Type))
                    .ThenByDescending(x => x.VirtualMachine!.Storage),
            "storageDesc" => query
                    .OrderByDescending(x => sortingList.IndexOf(x.Type))
                    .ThenByDescending(x => x.VirtualMachine!.Storage),
            _ => query
        };
    }
}
