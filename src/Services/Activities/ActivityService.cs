using Microsoft.EntityFrameworkCore;
using Persistence;
using Shared.Activities;
using Shared.VirtualMachines;

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
        var sortingList = new List<EActivity> { EActivity.Added, EActivity.Deleted };

        if (!string.IsNullOrWhiteSpace(request.SortingParameter))
        {
            switch(request.SortingParameter)
            {
                case "date":
                    query = query.OrderBy(x => x.CreatedAt);
                    break;
                case "cpu":
                    query = query
                        .OrderBy(x => sortingList.IndexOf((EActivity)x.Type))
                        .OrderBy(x => x.VirtualMachine!.CPU);
                    break;
                case "ram":
                    query = query
                        .OrderBy(x => sortingList.IndexOf((EActivity)x.Type))
                        .OrderBy(x => x.VirtualMachine!.RAM);
                    break;
                case "storage":
                    query = query
                        .OrderBy(x => sortingList.IndexOf((EActivity) x.Type))
                        .OrderBy(x => x.VirtualMachine!.Storage);
                    break;
                default:
                    break;
            }
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
                    BackupFrequency = (EBackupFrequency) a.VirtualMachine.BackupFrequency
                },
                Type = (EActivity) a.Type
            })
            .ToListAsync();

        return items;
    }
}
